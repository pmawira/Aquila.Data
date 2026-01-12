using Aquila.Data.Core.Query;
using Aquila.Data.Core.Storage;
using Xunit;

namespace Aquila.Data.Core.Tests
{
    public class JoinEngineTests
    {
        [Fact]
        public void InnerJoin_Returns_Matching_Rows()
        {
            var customers = new Table("Customers", "Id");
            var orders = new Table("Orders", "Id");

            customers.Insert(new() { ["Id"] = 1, ["Name"] = "Alice" });
            orders.Insert(new() { ["CustomerId"] = 1, ["Amount"] = 100 });

            var result = JoinEngine.InnerJoin(customers, orders, "Id", "CustomerId");

            Assert.Single(result);
            Assert.Equal("Alice", result.First()["Name"]);
            Assert.Equal(100, result.First()["Amount"]);
        }
    }
}
