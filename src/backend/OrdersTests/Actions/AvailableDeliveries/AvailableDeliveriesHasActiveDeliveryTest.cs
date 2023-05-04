using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesHasActiveDelivery;
using TestsBase;

namespace OrdersTests.Actions.AvailableDeliveries
{
    [TestClass()]
    public class AvailableDeliveriesHasActiveDeliveryTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task HasActiveDelivery_ReturnResponse_True()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<AvailableDeliveriesHasActiveDeliveryAction>();

            // Arrange
            var testingUser1 = AvailableDeliveriesHasActiveDeliveryFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = AvailableDeliveriesHasActiveDeliveryFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = AvailableDeliveriesHasActiveDeliveryFixture.CreateUserRole(testingUser1, RoleSeeder.Role5Deliverer.Id);

            var order = AvailableDeliveriesHasActiveDeliveryFixture.CreateOrder(toolkit);
            var delivery1 = AvailableDeliveriesHasActiveDeliveryFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse1PL, 1, order, toolkit);
            var delivery2 = AvailableDeliveriesHasActiveDeliveryFixture.CreateDelivery2(testingUser1, false, WarehouseSeeder.Warehouse1PL, 2, order, toolkit);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);
            context.Add(order);
            context.Add(delivery1);
            context.Add(delivery2);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(AvailableDeliveriesHasActiveDeliveryFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute();
            var result = executed.Value as AvailableDeliveriesHasActiveDeliveryResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result);
        }

        [TestMethod()]
        public async Task HasActiveDelivery_ReturnResponse_False()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<AvailableDeliveriesHasActiveDeliveryAction>();

            // Arrange
            var testingUser1 = AvailableDeliveriesHasActiveDeliveryFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = AvailableDeliveriesHasActiveDeliveryFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = AvailableDeliveriesHasActiveDeliveryFixture.CreateUserRole(testingUser1, RoleSeeder.Role5Deliverer.Id);

            var order = AvailableDeliveriesHasActiveDeliveryFixture.CreateOrder(toolkit);
            var delivery1 = AvailableDeliveriesHasActiveDeliveryFixture.CreateDelivery2(testingUser1, false, WarehouseSeeder.Warehouse1PL, 2, order, toolkit);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);
            context.Add(order);
            context.Add(delivery1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(AvailableDeliveriesHasActiveDeliveryFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute();
            var result = executed.Value as AvailableDeliveriesHasActiveDeliveryResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Result);
        }
    }
}
