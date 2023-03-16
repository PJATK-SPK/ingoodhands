using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Setup.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.OrdersActions.OrdersCancel;
using TestsBase;

namespace OrderTests.Actions.OrdersActionTest
{
    [TestClass()]
    public class OrdersCancelActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task OrdersCancelActionTest_CancelAction_ReturnsOkMessage()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<OrdersCancelAction>();

            //Arrange
            var testingUser1 = OrdersCancelActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = OrdersCancelActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = OrdersCancelActionFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            var order1 = OrdersCancelActionFixture.CreateOrder(testingUser1, AddressSeeder.Address1Poland, "ORD000001");
            var orderProduct1 = OrdersCancelActionFixture.CreateOrderProduct(order1, ProductSeeder.Product11Juice.Id, 100);
            var orderProduct2 = OrdersCancelActionFixture.CreateOrderProduct(order1, ProductSeeder.Product13Soup.Id, 50);

            var delivery1 = OrdersCancelActionFixture.CreateDelivery(testingUser1, AddressSeeder.Address2Poland, order1, true, WarehouseSeeder.Warehouse2PL);
            var delivery2 = OrdersCancelActionFixture.CreateDelivery(testingUser1, AddressSeeder.Address2Poland, order1, true, WarehouseSeeder.Warehouse2PL, "DEL000002");
            var deliveryProduct1 = OrdersCancelActionFixture.CreateDeliveryProduct(delivery1, ProductSeeder.Product11Juice.Id, 100);
            var deliveryProduct2 = OrdersCancelActionFixture.CreateDeliveryProduct(delivery2, ProductSeeder.Product13Soup.Id, 50);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            context.Add(order1);
            context.Add(orderProduct1);
            context.Add(orderProduct2);

            context.Add(delivery1);
            context.Add(delivery2);
            context.Add(deliveryProduct1);
            context.Add(deliveryProduct2);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(OrdersCancelActionFixture.GetCurrentUserInfo(testingAuth0User1));

            Assert.AreEqual(1, context.Orders.Where(c => c.IsCanceledByUser == false).Count());

            // Act
            var result = await action.Execute(toolkit.Hashids.EncodeLong(order1.Id));

            // Assert            
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(200, ((OkObjectResult)result).StatusCode);
            Assert.AreEqual(0, context.Orders.Where(c => c.IsCanceledByUser == false).Count());
            Assert.AreEqual(1, context.Orders.Where(c => c.IsCanceledByUser == true).Count());
        }

        [TestMethod()]
        public async Task OrdersCancelActionTest_CancelActionNotFoundOrderId_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<OrdersCancelAction>();

            //Arrange
            var testingUser1 = OrdersCancelActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = OrdersCancelActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = OrdersCancelActionFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(OrdersCancelActionFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(toolkit.Hashids.EncodeLong(1500)));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task OrdersCancelActionTest_CancelActionNoOrderProducts_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<OrdersCancelAction>();

            //Arrange
            var testingUser1 = OrdersCancelActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = OrdersCancelActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = OrdersCancelActionFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            var order1 = OrdersCancelActionFixture.CreateOrder(testingUser1, AddressSeeder.Address1Poland, "ORD000001");

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);
            context.Add(order1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(OrdersCancelActionFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(toolkit.Hashids.EncodeLong(order1.Id)));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}
