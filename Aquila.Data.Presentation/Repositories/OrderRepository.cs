using Aquila.Data.Core.Engine;
using Aquila.Data.Core.Exceptions;
using Aquila.Data.Core.Query;
using Aquila.Data.Core.Storage;
using Aquila.Data.Presentation.Infrastructure;
using Aquila.Data.Presentation.Models;

namespace Aquila.Data.Presentation.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Table _orders;
        private readonly Table _customers;
        private readonly DatabaseEngine db;
        private readonly ICustomerRepository _repo;
        public OrderRepository(ICustomerRepository repo)
        {
            _repo = repo;
             db = AquilaDatabase.Instance;
            // Ensure Customers exists (FK target)
            _customers = db.TableExists("Customers")
                ? db.Table("Customers")
                : db.CreateTable(
                    name: "Customers",
                    primaryKey: "Id",
                    uniqueKeys: "Name"
                );

            // Create Orders table
            _orders = db.TableExists("Orders")
                ? db.Table("Orders")
                : db.CreateTable(
                    name: "Orders",
                    primaryKey: "Id"
                );
        }

        public void Add(OrderViewModel order)
        {
            if(!db.TableExists("Customers"))
                throw new ConstraintViolationException("Customers does not exist");

            if(_repo.GetById(order.CustomerId) == null)
                throw new ConstraintViolationException($"Customer with ID: {order.CustomerId} does not exist");


            _orders.Insert(new Dictionary<string, object>
            {
                ["Id"] = order.Id,
                ["CustomerId"] = order.CustomerId,
                ["Amount"] = order.Amount
            });
        }

        public IEnumerable<OrderViewModel> GetOrdersWithUsers()
        {
            return JoinEngine.InnerJoin(_customers, _orders, "Id", "CustomerId")
                .Select(r => new OrderViewModel
                {
                    Id = (int)r["Orders.Id"],
                    CustomerId = (int)r["Orders.CustomerId"],
                    CustomerName = r["Customers.Name"].ToString()!,
                    Amount = (decimal)r["Orders.Amount"]
                });
        }
    }
}
