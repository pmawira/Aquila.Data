using Aquila.Data.Core.Storage;
using Aquila.Data.Presentation.Models;
using Aquila.Data.Presentation.Infrastructure;

namespace Aquila.Data.Presentation.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Table _customers;

        public CustomerRepository()
        {
            var db = AquilaDatabase.Instance;
            // Create table if not exists
            _customers = db.TableExists("Customers")
                ? db.Table("Customers")
                : db.CreateTable
                (
                  name: "Customers",
                  primaryKey: "Id",
                  uniqueKeys: "Name"
                );
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            return _customers.All().Select(r => new CustomerViewModel
            {
                Id = (int)r["Id"],
                Name = r["Name"].ToString()!
            });
        }

        public CustomerViewModel? GetById(int id)
        {
            var row = _customers.FindByKey("Id", id);
            if (row == null) return null;

            return new CustomerViewModel
            {
                Id = (int)row["Id"],
                Name = row["Name"].ToString()!
            };
        }

        public void Add(CustomerViewModel customer)
        {
            _customers.Insert(new ()
            {
                ["Id"] = customer.Id,
                ["Name"] = customer.Name
            });
        }

        public void Update(CustomerViewModel customer)
        {
            _customers.Update(
                r => (int)r["Id"] == customer.Id,
                r => r["Name"] = customer.Name
            );
        }

        public void Delete(int id)
        {
            _customers.Delete(r => (int)r["Id"] == id);
        }
    }
}
