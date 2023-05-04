using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.CurrentDelivery.CurrentDeliveryGetSingle;
using Orders.Actions.OrdersActions.OrdersGetSingle;
using OrdersTests.Actions.AvailableDeliveries;
using OrdersTests.Actions.Orders;
using TestsBase;

namespace OrdersTests.Actions.CurrentDelivery
{

    [TestClass()]
    public class CurrentDeliveryGetSingleTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task GetSingle_ReturnResponse()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CurrentDeliveryGetSingleAction>();

            // Arrange
            var testingUser1 = CurrentDeliveryGetSingleFixture.CreateUser("Normal", "User"); //warehouse1PL
            var testingAuth0User1 = CurrentDeliveryGetSingleFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = CurrentDeliveryGetSingleFixture.CreateUserRole(testingUser1, RoleSeeder.Role5Deliverer.Id);

            var order = CurrentDeliveryGetSingleFixture.CreateOrder(toolkit);
            var delivery1 = CurrentDeliveryGetSingleFixture.CreateDelivery(testingUser1, true, WarehouseSeeder.Warehouse1PL, 1, order, toolkit);//Delivered 2 products
            var delivery2 = CurrentDeliveryGetSingleFixture.CreateDelivery(testingUser1, true, WarehouseSeeder.Warehouse1PL, 2, order, toolkit);//Delivered 2 products
            var delivery3 = CurrentDeliveryGetSingleFixture.CreateDelivery2(testingUser1, false, WarehouseSeeder.Warehouse1PL, 5, order, toolkit);//notDelivered 4 products

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);
            context.Add(order);
            context.Add(delivery1);
            context.Add(delivery2);
            context.Add(delivery3);
            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(AvailableDeliveriesCountFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute();
            var result = executed.Value as CurrentDeliveryGetSingleResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual("Normal User", result.DelivererFullName);
            Assert.AreEqual("Service Service", result.NeedyFullName);
            Assert.AreEqual("DEL000005", result.DeliveryName);
            Assert.AreEqual("ORD000001", result.OrderName);
            Assert.AreEqual("80-503", result.WarehouseLocation.PostalCode);
            Assert.AreEqual("05-820", result.OrderLocation.PostalCode);
            Assert.AreEqual(4, result.Products.Count);
        }

        [TestMethod()]
        public async Task GetSingle_NoDelivery_ThrowException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CurrentDeliveryGetSingleAction>();

            //Arrange
            var testingUser1 = CurrentDeliveryGetSingleFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CurrentDeliveryGetSingleFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = CurrentDeliveryGetSingleFixture.CreateUserRole(testingUser1, RoleSeeder.Role5Deliverer.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CurrentDeliveryGetSingleFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute());

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}
