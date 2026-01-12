using Aquila.Data.Core.Engine;
using Aquila.Data.Core.Query;

namespace Aquila.Data.REPL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello, World!");
            var db = new DatabaseEngine();

            var customers = db.CreateTable("Customers", "Id");
            var orders = db.CreateTable("Orders", "Id");

            customers.Insert(new() { ["Id"] = 1, ["Name"] = "Alice" });
            customers.Insert(new() { ["Id"] = 2, ["Name"] = "Bob" });

            orders.Insert(new() { ["Id"] = 100, ["CustomerId"] = 1, ["Amount"] = 500 });

            Console.WriteLine("INNER JOIN RESULT:");

            var result = JoinEngine.InnerJoin(customers, orders, "Id", "CustomerId");

            foreach (var row in result)
            {
                Console.WriteLine(string.Join(", ", row.Select(x => $"{x.Key}:{x.Value}")));
            }
        }
    }
}
