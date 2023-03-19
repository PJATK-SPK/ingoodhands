using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.OrdersActions.OrdersGetSingle;
using TestsBase;

namespace OrdersTests.Actions.Orders
{
    [TestClass()]
    public class OrdersGetSingleTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task OrdersGetSingleTest_GetSingle_ReturnsResponse()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<OrdersGetSingleAction>();

            //Arrange
            var testingUser1 = OrdersGetSingleFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = OrdersGetSingleFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = OrdersGetSingleFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            var order1 = OrdersGetSingleFixture.CreateOrder(testingUser1, AddressSeeder.Address1Poland, "ORD000001");
            var orderProduct1 = OrdersGetSingleFixture.CreateOrderProduct(order1, ProductSeeder.Product11Juice.Id, 100);
            var orderProduct2 = OrdersGetSingleFixture.CreateOrderProduct(order1, ProductSeeder.Product13Soup.Id, 50);

            var delivery1 = OrdersGetSingleFixture.CreateDelivery(testingUser1, AddressSeeder.Address2Poland, order1, true, WarehouseSeeder.Warehouse2PL);
            var delivery2 = OrdersGetSingleFixture.CreateDelivery(testingUser1, AddressSeeder.Address2Poland, order1, true, WarehouseSeeder.Warehouse2PL, "DEL000002");
            var deliveryProduct1 = OrdersGetSingleFixture.CreateDeliveryProduct(delivery1, ProductSeeder.Product11Juice.Id, 100);
            var deliveryProduct2 = OrdersGetSingleFixture.CreateDeliveryProduct(delivery2, ProductSeeder.Product13Soup.Id, 50);

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

            toolkit.UpdateUserInfo(OrdersGetSingleFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute(toolkit.Hashids.EncodeLong(order1.Id));
            var result = executed.Value as OrdersGetSingleResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual("ORD000001", result!.Name);
            Assert.AreEqual(2, result!.Deliveries.Count);
            Assert.IsTrue(result!.Deliveries.Select(c => c.Name).Contains("DEL000002"));
            Assert.AreEqual(2, result!.Products.Count);
        }

        [TestMethod()]
        public async Task OrdersGetSingleTest_GetSingleNotFoundOrderId_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<OrdersGetSingleAction>();

            //Arrange
            var testingUser1 = OrdersGetSingleFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = OrdersGetSingleFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = OrdersGetSingleFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(OrdersGetSingleFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(toolkit.Hashids.EncodeLong(1500)));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}