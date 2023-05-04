using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesGetList;
using System.Linq.Dynamic.Core;
using TestsBase;
using WebApi.Controllers.Order;

namespace OrdersTests.Actions.AvailableDeliveries
{
    [TestClass()]
    public class AvailableDeliveriesGetListTest
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
            var action = toolkit.Resolve<AvailableDeliveriesGetListAction>();

            //Arrange
            var page = 1;
            var pageSize = 10;

            var testingUser1 = AvailableDeliveriesGetListFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = AvailableDeliveriesGetListFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = AvailableDeliveriesGetListFixture.CreateUserRole(testingUser1, RoleSeeder.Role5Deliverer.Id);
            var order = AvailableDeliveriesGetListFixture.CreateOrder(toolkit);
            var delivery1 = AvailableDeliveriesGetListFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse5DE, 1, order, toolkit);
            var delivery2 = AvailableDeliveriesGetListFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse1PL, 2, order, toolkit);
            var delivery3 = AvailableDeliveriesGetListFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse1PL, 3, order, toolkit);
            var delivery4 = AvailableDeliveriesGetListFixture.CreateDelivery2(testingUser1, false, WarehouseSeeder.Warehouse1PL, 4, order, toolkit);
            var delivery5 = AvailableDeliveriesGetListFixture.CreateDelivery2(testingUser1, false, WarehouseSeeder.Warehouse1PL, 5, order, toolkit);

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

            toolkit.UpdateUserInfo(AvailableDeliveriesGetListFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute(page, pageSize);
            var result = executed.Value as PagedResult<AvailableDeliveriesGetListResponse>;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(page, result.CurrentPage);
            Assert.AreEqual(pageSize, result.PageSize);
            Assert.IsTrue(result!.Queryable.Any());
            Assert.AreEqual("DEL000005", result!.Queryable.Single(c => toolkit.Hashids.DecodeSingleLong(c.Id) == delivery5.Id).DeliveryName);
            Assert.AreEqual("ORD000001", result!.Queryable.Single(c => toolkit.Hashids.DecodeSingleLong(c.Id) == delivery3.Id).OrderName);
            Assert.AreEqual(4, result!.Queryable.Count());
            Assert.AreEqual(4, result!.Queryable.Single(c => toolkit.Hashids.DecodeSingleLong(c.Id) == delivery4.Id).ProductTypesCount);
            Assert.AreEqual(2, result!.Queryable.Single(c => toolkit.Hashids.DecodeSingleLong(c.Id) == delivery2.Id).ProductTypesCount);
        }
    }
}
