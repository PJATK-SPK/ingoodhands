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
            var abc = new RemoveThisClass();
            Assert.AreEqual(3, abc.TestMe(1, 2));
        }
    }
}