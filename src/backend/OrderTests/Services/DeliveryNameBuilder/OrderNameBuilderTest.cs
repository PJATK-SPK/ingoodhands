using Core.Exceptions;
using Autofac;
using Orders;
using Orders.Services.DeliveryNameBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace OrderTests.Services.DeliveryNameBuilder
{
    [TestClass()]
    public class DeliveryNameBuilderServiceTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new OrdersModule(),
        };

        [TestMethod()]
        public void OrderModuleTest_DeliveryIdWithing6DigitsInRange()
        {
            using var toolkit = new TestsToolkit(_usedModules, TestType.Unit);
            var action = toolkit.Resolve<DeliveryNameBuilderService>();

            long id = 123456;
            string expected = "DEL123456";

            // Act
            string actual = action.Build(id);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void OrderModuleTest_DeliveryWithLessThanSixDigitId()
        {
            using var toolkit = new TestsToolkit(_usedModules, TestType.Unit);
            var action = toolkit.Resolve<DeliveryNameBuilderService>();

            // Arrange
            long id = 55;
            string expected = "DEL000055";

            // Act
            string actual = action.Build(id);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void OrderModuleTest_DeliveryWithInvalidId()
        {
            using var toolkit = new TestsToolkit(_usedModules, TestType.Unit);
            var action = toolkit.Resolve<DeliveryNameBuilderService>();

            // Arrange
            long id = 1000000;
            string expectedErrorMessage = "Delivery id is out of range. Please, contact system administrator";

            // Act and Assert
            var exception = Assert.ThrowsException<ApplicationErrorException>(() => action.Build(id));
            Assert.AreEqual(expectedErrorMessage, exception.Message);
        }

        [TestMethod()]
        public void OrderModuleTest_DeliveryWithInvalidIdLowerThan1()
        {
            using var toolkit = new TestsToolkit(_usedModules, TestType.Unit);
            var action = toolkit.Resolve<DeliveryNameBuilderService>();

            // Arrange
            long id = -1;
            string expectedErrorMessage = "Delivery id is out of range. Please, contact system administrator";

            // Act and Assert
            var exception = Assert.ThrowsException<ApplicationErrorException>(() => action.Build(id));
            Assert.AreEqual(expectedErrorMessage, exception.Message);
        }
    }
}