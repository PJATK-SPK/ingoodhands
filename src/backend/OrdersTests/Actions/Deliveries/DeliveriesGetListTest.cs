using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.DeliveriesActions.DeliveriesGetList;
using System.Linq.Dynamic.Core;
using TestsBase;

namespace OrdersTests.Actions.Deliveries
{
    [TestClass()]
    public class DeliveriesGetListTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task GetListTest_ReturnResponse()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<DeliveriesGetListAction>();

            //Arrange
            var page = 1;
            var pageSize = 10;

            var testingUser1 = DeliveriesGetListFixture.CreateUser("Normal", "User", WarehouseSeeder.Warehouse2PL.Id);
            var testingAuth0User1 = DeliveriesGetListFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = DeliveriesGetListFixture.CreateUserRole(testingUser1, RoleSeeder.Role4WarehouseKeeper.Id);
            var order = DeliveriesGetListFixture.CreateOrder(toolkit);
            var delivery1 = DeliveriesGetListFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse2PL, 1, order, toolkit);//status active 2 products
            var delivery2 = DeliveriesGetListFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse9FR, 2, order, toolkit);//status active 2 products
            var delivery3 = DeliveriesGetListFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse5DE, 3, order, toolkit);//status active 2 products
            var delivery4 = DeliveriesGetListFixture.CreateDelivery2(testingUser1, false, WarehouseSeeder.Warehouse7CZ, 4, order, toolkit);//status inactive 4 products
            var delivery5 = DeliveriesGetListFixture.CreateDelivery2(testingUser1, false, WarehouseSeeder.Warehouse6HU, 5, order, toolkit);//status inactive 4 products

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

            toolkit.UpdateUserInfo(DeliveriesGetListFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute(page, pageSize);
            var result = executed.Value as PagedResult<DeliveriesGetListResponseItem>;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(page, result.CurrentPage);
            Assert.AreEqual(pageSize, result.PageSize);
            Assert.IsTrue(result!.Queryable.Any());
            Assert.AreEqual(1, result!.Queryable.Count());
        }
    }
}
