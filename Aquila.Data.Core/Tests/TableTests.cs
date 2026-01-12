using Aquila.Data.Core.Storage;
using Xunit;

namespace Aquila.Data.Core.Tests
{
    
    public class TableTests
    {
        [Fact]
        public void Insert_And_Select_Works()
        {
            var table = new Table("Test", "Id");

            table.Insert(new Dictionary<string, object>
            {
                ["Id"] = 1,
                ["Name"] = "Alice"
            });

            var result = table.All()
                              .First(r => (int)r["Id"] == 1);

            Assert.Equal("Alice", result["Name"]);
        }

        [Fact]
        public void Update_Works()
        {
            var table = new Table("Test", "Id");

            table.Insert(new()
            {
                ["Id"] = 1,
                ["Name"] = "Alice"
            });

            table.Update(
                r => (int)r["Id"] == 1,
                r => r["Name"] = "Bob"
            );

            var updated = table.All()
                               .First(r => (int)r["Id"] == 1);

            Assert.Equal("Bob", updated["Name"]);
        }

        [Fact]
        public void Delete_Works()
        {
            var table = new Table("Test", "Id");

            table.Insert(new()
            {
                ["Id"] = 1
            });

            table.Delete(r => (int)r["Id"] == 1);

            Assert.Empty(table.All());
        }
    }
}
