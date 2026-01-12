using Aquila.Data.Core.Storage;
using Aquila.Data.Core.Exceptions;
using Xunit;


namespace Aquila.Data.Core.Tests
{
    public class ConstraintTests
    {
        [Fact]
        
        public void Duplicate_Primary_Key_Is_Rejected()
        {
            var table = new Table(
                name: "Users",
                primaryKey: "Id"
            );

            table.Insert(new()
            {
                ["Id"] = 1
            });

            Assert.Throws<ConstraintViolationException>(() =>
                table.Insert(new()
                {
                    ["Id"] = 1
                }));
        }

        [Fact]
        public void Duplicate_Unique_Key_Is_Rejected()
        {
            var table = new Table(
                "Users",
                primaryKey: "Id",
                uniqueKeys: new[] { "Email" }
            );

            table.Insert(new()
            {
                ["Id"] = 1,
                ["Email"] = "a@test.com"
            });

            Assert.Throws<ConstraintViolationException>(() =>
                table.Insert(new()
                {
                    ["Id"] = 2,
                    ["Email"] = "a@test.com"
                }));
        }
    }
}
