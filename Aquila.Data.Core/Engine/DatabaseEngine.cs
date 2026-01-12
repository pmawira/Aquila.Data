using Aquila.Data.Core.Storage;


namespace Aquila.Data.Core.Engine
{
    public class DatabaseEngine
    {
        private readonly Dictionary<string, Table> _tables = new();

        public Table CreateTable(string name, string primaryKey, params string[] uniqueKeys)
        {
            var table = new Table(name, primaryKey, uniqueKeys);
            _tables[name] = table;
            return table;
        }

       
        public Table Table(string name) => _tables[name];
        public bool TableExists(string name)
        {
            return _tables.ContainsKey(name);
        }

    }
}
