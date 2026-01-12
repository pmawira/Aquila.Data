using Aquila.Data.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aquila.Data.Core.Storage
{
    public class Table
    {
        public string Name { get; }
        public string _primaryKey { get; }
        public IReadOnlyCollection<string> UniqueKeys => _uniqueKeys.AsReadOnly();

        private readonly List<Dictionary<string, object>> _rows = new();
        private readonly List<string> _uniqueKeys = new();

        // Simple in-memory index (column -> value -> rows)
        private readonly Dictionary<string, Dictionary<object, List<Dictionary<string, object>>>> _indexes
            = new();
        public Table(
    string name,
    string primaryKey,
    IEnumerable<string>? uniqueKeys = null)
        {
            Name = name;
            _primaryKey = primaryKey;
            _uniqueKeys = uniqueKeys?.ToList() ?? new List<string>();

            _indexes = new();

            // Primary Key index
            _indexes[_primaryKey] = new();

            // Unique indexes
            foreach (var key in _uniqueKeys)
            {
                _indexes[key] = new();
            }
        }

        public void Insert(Dictionary<string, object> row)
        {
            //  PRIMARY KEY enforcement
            if (!row.ContainsKey(_primaryKey))
                throw new ConstraintViolationException(
                    $"Primary key '{_primaryKey}' is required."
                );

            var pkValue = row[_primaryKey];

            if (_indexes[_primaryKey].ContainsKey(pkValue))
                throw new ConstraintViolationException(
                    $"Duplicate primary key value '{pkValue}'."
                );

            // 🔒 UNIQUE constraints
            foreach (var key in _uniqueKeys)
            {
                if (!row.ContainsKey(key))
                    throw new ConstraintViolationException(
                        $"Unique key '{key}' is required."
                    );

                var value = row[key];

                if (_indexes[key].ContainsKey(value))
                    throw new ConstraintViolationException(
                        $"Duplicate value '{value}' for unique key '{key}'."
                    );
            }

            // Insert row
            _rows.Add(row);

            // Index PK
            _indexes[_primaryKey][pkValue] =
                new List<Dictionary<string, object>> { row };

            // Index uniques
            foreach (var key in _uniqueKeys)
            {
                var value = row[key];
                _indexes[key][value] =
                    new List<Dictionary<string, object>> { row };
            }
        }

        public IEnumerable<Dictionary<string, object>> All() => _rows;

        public Dictionary<string, object>? FindByKey(string key, object value)
        {
            return _indexes.ContainsKey(key) && _indexes[key].ContainsKey(value)
                ? _indexes[key][value].First()
                : null;
        }

        public void Update(Func<Dictionary<string, object>, bool> predicate,
                           Action<Dictionary<string, object>> update)
        {
            foreach (var row in _rows.Where(predicate))
                update(row);
        }

        public void Delete(Func<Dictionary<string, object>, bool> predicate)
        {
            var toRemove = _rows.Where(predicate).ToList();
            foreach (var row in toRemove)
            {
                foreach (var key in _uniqueKeys)
                {
                    _indexes[key].Remove(row[key]);
                }
                _rows.Remove(row);
            }
        }
    }
}
