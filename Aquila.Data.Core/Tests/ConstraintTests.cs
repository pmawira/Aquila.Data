using Aquila.Data.Core.Storage;
using Aquila.Data.Core.Engine;
using Aquila.Data.Core.Exceptions;
using Xunit;


namespace Aquila.Data.Core.Tests
{
    public class ConstraintTests
    {
        [Fact]
        
        public void Duplicate_Primary_Key_Is_Rejected()
        {

           
            var db = new DatabaseEngine();
            var customers = db.CreateTable("Customers", "Id");


            customers.Insert(new()
            {
                ["Id"] = 1
            });

            Assert.Throws<ConstraintViolationException>(() =>
                customers.Insert(new()
                {
                    ["Id"] = 1
                }));
        }

        [Fact]
        public void Duplicate_Unique_Key_Is_Rejected()
        {
         
            var db = new DatabaseEngine();
            var customers = db.CreateTable("Users", "Id", new[] { "Name" });
            customers.Insert(new()
            {
                ["Id"] = 1,
                ["Name"] = "Peter"
            });

            Assert.Throws<ConstraintViolationException>(() =>
                customers.Insert(new()
                {
                    ["Id"] = 2,
                    ["Name"] = "Peter"
                }));
        }
    }
}
