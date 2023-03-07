using Core.Exceptions;
using Autofac;
using Order;
using Order.Services.OrderNameBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace OrderTests.Services.OrderNameBuilder
{
    [TestClass()]
    public class OrderNameBuilderServiceTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new OrderModule(),
        };

        [TestMethod()]
        public void OrderModuleTest_IdWithing6DigitsInRange()
        {
            using var toolkit = new TestsToolkit(_usedModules, TestType.Unit);
            var action = toolkit.Resolve<OrderNameBuilderService>();

            long id = 123456;
            string expected = "ORD123456";

            // Act
            string actual = action.Build(id);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void OrderModuleTest_WithLessThanSixDigitId()
        {
            using var toolkit = new TestsToolkit(_usedModules, TestType.Unit);
            var action = toolkit.Resolve<OrderNameBuilderService>();

            // Arrange
            long id = 55;
            string expected = "ORD000055";

            // Act
            string actual = action.Build(id);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void OrderModuleTest_WithInvalidId()
        {
            using var toolkit = new TestsToolkit(_usedModules, TestType.Unit);
            var action = toolkit.Resolve<OrderNameBuilderService>();

            // Arrange
            long id = 1000000;
            string expectedErrorMessage = "Order id is out of range. Please, contact system administrator";

            // Act and Assert
            var exception = Assert.ThrowsException<ApplicationErrorException>(() => action.Build(id));
            Assert.AreEqual(expectedErrorMessage, exception.Message);
        }

        [TestMethod()]
        public void OrderModuleTest_WithInvalidIdLowerThan1()
        {
            using var toolkit = new TestsToolkit(_usedModules, TestType.Unit);
            var action = toolkit.Resolve<OrderNameBuilderService>();

            // Arrange
            long id = -1;
            string expectedErrorMessage = "Order id is out of range. Please, contact system administrator";

            // Act and Assert
            var exception = Assert.ThrowsException<ApplicationErrorException>(() => action.Build(id));
            Assert.AreEqual(expectedErrorMessage, exception.Message);
        }
    }
}