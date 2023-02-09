using Donate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DonateTests
{
    [TestClass()]
    public class RemoveThisClassTests
    {
        [TestMethod()]
        public void TestMeTest()
        {
            Assert.AreEqual(3, RemoveThisClass.TestMe(1, 2));
        }
    }
}