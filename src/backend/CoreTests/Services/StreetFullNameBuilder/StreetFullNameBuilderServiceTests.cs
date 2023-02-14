using Core.Services.StreetFullNameBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests.Services.StreetFullNameBuilder
{
    [TestClass()]
    public class StreetFullNameBuilderServiceTests
    {
        [TestMethod()]
        public void Check_all_cases()
        {
            Assert.AreEqual("Jaworowa 3/5", StreetFullNameBuilderService.Build("Jaworowa", "3", "5"));
            Assert.AreEqual("Jaworowa 3", StreetFullNameBuilderService.Build("Jaworowa", "3"));
            Assert.AreEqual("Jaworowa", StreetFullNameBuilderService.Build("Jaworowa"));
            Assert.AreEqual("Jaworowa ?/5", StreetFullNameBuilderService.Build("Jaworowa", null, "5"));
            Assert.AreEqual("?/5", StreetFullNameBuilderService.Build(null, null, "5"));
            Assert.AreEqual("3/5", StreetFullNameBuilderService.Build(null, "3", "5"));
            Assert.AreEqual("3", StreetFullNameBuilderService.Build(null, "3"));
        }
    }
}