using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DataBulkManager.MSSQL.Tests
{
    [TestClass]
    public class BulkTest
    {
        [TestMethod]
        public void AttributeTestMethod1()
        {
            Assert.IsTrue(typeof(Entity).GetBulkTableName() == "test.TestEntity");
        }

        [TestMethod]
        public void AttributeTestMethod2()
        {
            Assert.IsTrue(typeof(SimpleEntity).GetBulkTableName() == "dbo.SimpleEntity");
        }

        [TestMethod]
        public void AttributeTestMethod3()
        {
            var attributes = typeof(Entity).GetBulkColumnAttributes();
            Assert.IsTrue(attributes.Count() == 4);
        }

        [TestMethod]
        public void AttributeTestMethod4()
        {
            var attributes = typeof(SimpleEntity).GetBulkColumnAttributes();
            Assert.IsTrue(attributes.Count() == 1);
        }

        [TestMethod]
        public void AttributeTestMethod5()
        {
            SimpleEntity entity = new SimpleEntity()
            {
                Name = nameof(SimpleEntity)
            };
            Assert.IsTrue(entity.GetBulkEntityValues().Length == 1);
            Assert.IsTrue(entity.GetBulkEntityValues().FirstOrDefault()?.ToString() == nameof(SimpleEntity));
        }
    }
}
