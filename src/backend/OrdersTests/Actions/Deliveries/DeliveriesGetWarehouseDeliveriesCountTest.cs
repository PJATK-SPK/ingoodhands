using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.DeliveriesActions.DeliveriesGetWarehouseDeliveriesCount;
using TestsBase;

namespace OrdersTests.Actions.Deliveries
{
    [TestClass()]
    public class DeliveriesGetWarehouseDeliveriesCountTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task GetCount_ReturnResponse()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<DeliveriesGetWarehouseDeliveriesCountAction>();

            // Arrange
            var testingUser1 = DeliveriesGetWarehouseDeliveriesCountFixture.CreateUser("Normal", "User"); //warehouse1PL
            var testingAuth0User1 = DeliveriesGetWarehouseDeliveriesCountFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = DeliveriesGetWarehouseDeliveriesCountFixture.CreateUserRole(testingUser1, RoleSeeder.Role4WarehouseKeeper.Id);

            var order = DeliveriesGetWarehouseDeliveriesCountFixture.CreateOrder(toolkit);
            var delivery1 = DeliveriesGetWarehouseDeliveriesCountFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse1PL, 1, order, toolkit);
            var delivery2 = DeliveriesGetWarehouseDeliveriesCountFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse1PL, 2, order, toolkit);

            var order2 = DeliveriesGetWarehouseDeliveriesCountFixture.CreateOrder2(toolkit);
            var delivery3 = DeliveriesGetWarehouseDeliveriesCountFixture.CreateDelivery2(testingUser1, false, WarehouseSeeder.Warehouse1PL, 3, order2, toolkit);
            var delivery4 = DeliveriesGetWarehouseDeliveriesCountFixture.CreateDelivery2(testingUser1, false, WarehouseSeeder.Warehouse1PL, 4, order2, toolkit);
            var delivery5 = DeliveriesGetWarehouseDeliveriesCountFixture.CreateDelivery2(testingUser1, false, WarehouseSeeder.Warehouse1PL, 5, order2, toolkit);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);
            context.Add(order);
            context.Add(order2);
            context.Add(delivery1);
            context.Add(delivery2);
            context.Add(delivery3);
            context.Add(delivery4);
            context.Add(delivery5);
            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(DeliveriesGetWarehouseDeliveriesCountFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute();
            var result = executed.Value as DeliveriesGetWarehouseDeliveriesCountResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }


        [TestMethod()]
        public async Task GetCount_Return0()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<DeliveriesGetWarehouseDeliveriesCountAction>();

            // Arrange
            var testingUser1 = DeliveriesGetWarehouseDeliveriesCountFixture.CreateUser2("Normal", "User"); //noWarehouse
            var testingAuth0User1 = DeliveriesGetWarehouseDeliveriesCountFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = DeliveriesGetWarehouseDeliveriesCountFixture.CreateUserRole(testingUser1, RoleSeeder.Role4WarehouseKeeper.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(DeliveriesGetWarehouseDeliveriesCountFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute();
            var result = executed.Value as DeliveriesGetWarehouseDeliveriesCountResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsNull(context.Users.Single(c => c.Id == testingUser1.Id).Warehouse);
            Assert.AreEqual(0, result.Count);
        }
    }
}