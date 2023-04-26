using Autofac;
using Core;
using Core.Database;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Setup.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.DeliveriesActions.DeliveriesGetList;
using Orders.Actions.DeliveriesActions.DeliveriesGetSingle;
using Orders.Actions.OrdersActions.OrdersGetSingle;
using OrdersTests.Actions.Orders;
using System.Drawing.Printing;
using System.Linq.Dynamic.Core;
using TestsBase;

namespace OrdersTests.Actions.Deliveries
{
    [TestClass()]
    public class DeliveriesGetSingleTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task GetSingleListTest_ReturnResponse()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<DeliveriesGetSingleAction>();

            // Arrange
            var testingUser1 = DeliveriesGetListFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = DeliveriesGetListFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = DeliveriesGetListFixture.CreateUserRole(testingUser1, RoleSeeder.Role4WarehouseKeeper.Id);
            var order = DeliveriesGetListFixture.CreateOrder(toolkit);
            var delivery1 = DeliveriesGetListFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse2PL, 1, order, toolkit);//status active 2 products
            var delivery2 = DeliveriesGetListFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse9FR, 2, order, toolkit);//status active 2 products
            var delivery3 = DeliveriesGetListFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse5DE, 3, order, toolkit);//status active 2 products
            var delivery4 = DeliveriesGetListFixture.CreateDelivery2(testingUser1, false, WarehouseSeeder.Warehouse7CZ, 4, order, toolkit);//status inactive 4 products

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);
            context.Add(order);
            context.Add(delivery1);
            context.Add(delivery2);
            context.Add(delivery3);
            context.Add(delivery4);
            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(DeliveriesGetListFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute(toolkit.Hashids.EncodeLong(delivery4.Id));
            var result = executed.Value as DeliveriesGetSingleResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Products.Any());
            Assert.AreEqual("DEL000004", result!.DeliveryName);
            Assert.AreEqual(4, result!.Products.Count);
            Assert.AreEqual("Czech Republic", result!.CountryName);
            Assert.AreEqual(toolkit.Hashids.Encode((int)delivery4.Id), result.Id);
            Assert.IsTrue(result.Products.Any(c => c.Unit == "kg"));
            Assert.IsTrue(result.Products.Any(c => c.Unit == "l"));
        }

        [TestMethod()]
        public async Task GetSingleNotFoundDeliveryId_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<DeliveriesGetSingleAction>();

            //Arrange
            var testingUser1 = DeliveriesGetListFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = DeliveriesGetListFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = DeliveriesGetListFixture.CreateUserRole(testingUser1, RoleSeeder.Role4WarehouseKeeper.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(DeliveriesGetListFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(toolkit.Hashids.EncodeLong(1500)));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}

