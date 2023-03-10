using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;

namespace OrdersTests.Services.StreetFullNameBuilder
{
    [TestClass()]
    public class DeleteMeTests
    {
        [TestMethod()]
        public void Check_all_cases()
        {
            Assert.AreEqual(3, DeleteMe.Test(1, 2));
        }
    }
}