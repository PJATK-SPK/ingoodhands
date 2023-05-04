using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesAssignDelivery;
using TestsBase;

namespace OrdersTests.Actions.AvailableDeliveries
{
    [TestClass()]
    public class AvailableDeliveriesAssignDeliveryTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task AssignUser_To_Delivery()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<AvailableDeliveriesAssignDeliveryAction>();

            // Arrange
            var testingUser1 = AvailableDeliveriesAssignDeliveryFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = AvailableDeliveriesAssignDeliveryFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = AvailableDeliveriesAssignDeliveryFixture.CreateUserRole(testingUser1, RoleSeeder.Role5Deliverer.Id);

            var order = AvailableDeliveriesAssignDeliveryFixture.CreateOrder(toolkit);
            var delivery1 = AvailableDeliveriesAssignDeliveryFixture.CreateDelivery(false, WarehouseSeeder.Warehouse1PL, 1, order, toolkit);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);
            context.Add(order);
            context.Add(delivery1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(AvailableDeliveriesAssignDeliveryFixture.GetCurrentUserInfo(testingAuth0User1));

            Assert.IsNull(context.Deliveries.Single(c => c.Id == delivery1.Id).DelivererUserId);

            // Act
            var executed = await action.Execute(toolkit.Hashids.EncodeLong(delivery1.Id));
            var result = executed.Value;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(testingUser1.Id, context.Deliveries.Single(c => c.Id == delivery1.Id).DelivererUserId);
        }

        [TestMethod()]
        public async Task AssignUser_Cannot_Find_Delivery_ThrowsError()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<AvailableDeliveriesAssignDeliveryAction>();

            //Arrange
            var testingUser1 = AvailableDeliveriesAssignDeliveryFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = AvailableDeliveriesAssignDeliveryFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = AvailableDeliveriesAssignDeliveryFixture.CreateUserRole(testingUser1, RoleSeeder.Role5Deliverer.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(AvailableDeliveriesAssignDeliveryFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(toolkit.Hashids.EncodeLong(1500)));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}
