using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
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