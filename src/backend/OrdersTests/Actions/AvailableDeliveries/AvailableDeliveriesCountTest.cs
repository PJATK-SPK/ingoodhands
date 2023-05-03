using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesCount;
using TestsBase;

namespace OrdersTests.Actions.AvailableDeliveries
{
    [TestClass()]
    public class AvailableDeliveriesCountTest
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
            var action = toolkit.Resolve<AvailableDeliveriesCountAction>();

            // Arrange
            var testingUser1 = AvailableDeliveriesCountFixture.CreateUser("Normal", "User"); //warehouse1PL
            var testingAuth0User1 = AvailableDeliveriesCountFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = AvailableDeliveriesCountFixture.CreateUserRole(testingUser1, RoleSeeder.Role5Deliverer.Id);

            var order = AvailableDeliveriesCountFixture.CreateOrder(toolkit);
            var delivery1 = AvailableDeliveriesCountFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse1PL, 1, order, toolkit);//status active 2 products
            var delivery2 = AvailableDeliveriesCountFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse1PL, 2, order, toolkit);//status active 2 products
            var delivery3 = AvailableDeliveriesCountFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse1PL, 3, order, toolkit);//status active 2 products
            var delivery4 = AvailableDeliveriesCountFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse1PL, 4, order, toolkit);//status active 2 products
            var delivery5 = AvailableDeliveriesCountFixture.CreateDelivery2(testingUser1, false, WarehouseSeeder.Warehouse1PL, 5, order, toolkit);//status inactive 4 products

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);
            context.Add(order);
            context.Add(delivery1);
            context.Add(delivery2);
            context.Add(delivery3);
            context.Add(delivery4);
            context.Add(delivery5);
            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(AvailableDeliveriesCountFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute();
            var result = executed.Value as AvailableDeliveriesCountResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
        }


        [TestMethod()]
        public async Task GetCount_Return0()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<AvailableDeliveriesCountAction>();

            // Arrange
            var testingUser1 = AvailableDeliveriesCountFixture.CreateUser2("Normal", "User"); //noWarehouse
            var testingAuth0User1 = AvailableDeliveriesCountFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = AvailableDeliveriesCountFixture.CreateUserRole(testingUser1, RoleSeeder.Role5Deliverer.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(AvailableDeliveriesCountFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute();
            var result = executed.Value as AvailableDeliveriesCountResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsNull(context.Users.Single(c => c.Id == testingUser1.Id).Warehouse);
            Assert.AreEqual(0, result.Count);
        }
    }
}