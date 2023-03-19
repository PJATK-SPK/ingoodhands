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

namespace OrdersTests.Actions.Orders
{
    [TestClass()]
    public class OrdersCancelTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task OrdersCancelTest_CancelAction_ReturnsOkMessage()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<OrdersCancelAction>();

            //Arrange
            var testingUser1 = OrdersCancelFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = OrdersCancelFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = OrdersCancelFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            var order1 = OrdersCancelFixture.CreateOrder(testingUser1, AddressSeeder.Address1Poland, "ORD000001");
            var orderProduct1 = OrdersCancelFixture.CreateOrderProduct(order1, ProductSeeder.Product11Juice.Id, 100);
            var orderProduct2 = OrdersCancelFixture.CreateOrderProduct(order1, ProductSeeder.Product13Soup.Id, 50);

            var delivery1 = OrdersCancelFixture.CreateDelivery(testingUser1, AddressSeeder.Address2Poland, order1, true, WarehouseSeeder.Warehouse2PL);
            var delivery2 = OrdersCancelFixture.CreateDelivery(testingUser1, AddressSeeder.Address2Poland, order1, true, WarehouseSeeder.Warehouse2PL, "DEL000002");
            var deliveryProduct1 = OrdersCancelFixture.CreateDeliveryProduct(delivery1, ProductSeeder.Product11Juice.Id, 100);
            var deliveryProduct2 = OrdersCancelFixture.CreateDeliveryProduct(delivery2, ProductSeeder.Product13Soup.Id, 50);

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

            toolkit.UpdateUserInfo(OrdersCancelFixture.GetCurrentUserInfo(testingAuth0User1));

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
        public async Task OrdersCancelTest_CancelNotFoundOrderId_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<OrdersCancelAction>();

            //Arrange
            var testingUser1 = OrdersCancelFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = OrdersCancelFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = OrdersCancelFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(OrdersCancelFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(toolkit.Hashids.EncodeLong(1500)));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task OrdersCancelTest_CancelNoOrderProducts_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<OrdersCancelAction>();

            //Arrange
            var testingUser1 = OrdersCancelFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = OrdersCancelFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = OrdersCancelFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            var order1 = OrdersCancelFixture.CreateOrder(testingUser1, AddressSeeder.Address1Poland, "ORD000001");

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);
            context.Add(order1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(OrdersCancelFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(toolkit.Hashids.EncodeLong(order1.Id)));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}
