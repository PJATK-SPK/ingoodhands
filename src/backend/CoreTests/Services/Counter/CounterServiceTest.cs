using Autofac;
using Core;
using Core.Database;
using Core.Database.Enums;
using Core.Services;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace CoreTests.Services.Counter
{
    [TestClass()]
    public class CounterServiceTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None)
        };

        [TestMethod()]
        public async Task CounterServiceTest_DonationTest_ReturnCounterOfDonationTable()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CounterService>();

            // Arrange
            var donationTableName = TableName.Donations;

            // Act
            var result = await action.GetCounter(donationTableName);

            // Assert
            Assert.AreEqual(0, result.Value);
        }

        [TestMethod()]
        public async Task CounterServiceTest_UpdateDonationCounter_ReturnUpdatedCounterOfDonationTable()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CounterService>();

            // Arrange
            var donationTableName = TableName.Donations;

            // Act
            var result = await action.GetAndUpdateNextCounter(donationTableName);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public async Task CounterServiceTest_OrderTest_ReturnCounterOfOrderTable()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CounterService>();

            // Arrange
            var orderTableName = TableName.Orders;

            // Act
            var result = await action.GetCounter(orderTableName);

            // Assert
            Assert.AreEqual(0, result.Value);
        }

        [TestMethod()]
        public async Task CounterServiceTest_UpdateOrderCounter_ReturnUpdatedCounterOfOrderTable()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CounterService>();

            // Arrange
            var orderTableName = TableName.Donations;

            // Act
            var result = await action.GetAndUpdateNextCounter(orderTableName);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public async Task CounterServiceTest_DeliveryTest_ReturnCounterOfDeliveryTable()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CounterService>();

            // Arrange
            var deliveryTableName = TableName.Deliveries;

            // Act
            var result = await action.GetCounter(deliveryTableName);

            // Assert
            Assert.AreEqual(0, result.Value);
        }

        [TestMethod()]
        public async Task CounterServiceTest_UpdateDeliveryCounter_ReturnUpdatedCounterOfDeliveryTable()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CounterService>();

            // Arrange
            var deliveryTableName = TableName.Deliveries;

            // Act
            var result = await action.GetAndUpdateNextCounter(deliveryTableName);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
