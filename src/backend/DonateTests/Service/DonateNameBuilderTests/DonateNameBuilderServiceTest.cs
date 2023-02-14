using Core.Exceptions;
using Autofac;
using Donate;
using Donate.Services.DonateNameBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace DonateTests.Service.DonateNameBuilderTests
{
    [TestClass()]
    public class DonateNameBuilderServiceTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new DonateModule(),
        };

        [TestMethod()]
        public async Task DonateModuleTest_IdWithing6DigitsInRange()
        {
            using var toolkit = new TestsToolkit(_usedModules, TestType.Unit);
            var action = toolkit.Resolve<DonateNameBuilderService>();

            long id = 123456;
            string expected = "DNT123456";

            // Act
            string actual = await action.DonateNameBuilder(id);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public async Task DonateModuleTest_WithLessThanSixDigitId()
        {
            using var toolkit = new TestsToolkit(_usedModules, TestType.Unit);
            var action = toolkit.Resolve<DonateNameBuilderService>();

            // Arrange
            long id = 55;
            string expected = "DNT000055";

            // Act
            string actual = await action.DonateNameBuilder(id);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public async Task DonateModuleTest_WithInvalidId()
        {
            using var toolkit = new TestsToolkit(_usedModules, TestType.Unit);
            var action = toolkit.Resolve<DonateNameBuilderService>();

            // Arrange
            long id = 1000000;
            string expectedErrorMessage = "Donation id is out of range. Please, contact system administrator";

            // Act and Assert
            var exception = await Assert.ThrowsExceptionAsync<HttpError500Exception>(() => action.DonateNameBuilder(id));
            Assert.AreEqual(expectedErrorMessage, exception.Message);
        }

        [TestMethod()]
        public async Task DonateModuleTest_WithInvalidIdLowerThan1()
        {
            using var toolkit = new TestsToolkit(_usedModules, TestType.Unit);
            var action = toolkit.Resolve<DonateNameBuilderService>();

            // Arrange
            long id = -1;
            string expectedErrorMessage = "Donation id is out of range. Please, contact system administrator";

            // Act and Assert
            var exception = await Assert.ThrowsExceptionAsync<HttpError500Exception>(() => action.DonateNameBuilder(id));
            Assert.AreEqual(expectedErrorMessage, exception.Message);
        }
    }
}
