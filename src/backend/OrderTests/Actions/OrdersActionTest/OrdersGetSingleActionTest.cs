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

namespace OrderTests.Actions.OrdersActionTest
{
    [TestClass()]
    public class OrdersGetSingleActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task OrdersGetSingleActionTest_GetSingleAction_ReturnsResponse()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<OrdersGetSingleAction>();

            //Arrange
            var testingUser1 = OrdersGetSingleActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = OrdersGetSingleActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = OrdersGetSingleActionFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            var order1 = OrdersGetSingleActionFixture.CreateOrder(testingUser1, AddressSeeder.Address1Poland, "ORD000001");
            var orderProduct1 = OrdersGetSingleActionFixture.CreateOrderProduct(order1, ProductSeeder.Product11Juice.Id, 100);
            var orderProduct2 = OrdersGetSingleActionFixture.CreateOrderProduct(order1, ProductSeeder.Product13Soup.Id, 50);

            var delivery1 = OrdersGetSingleActionFixture.CreateDelivery(testingUser1, AddressSeeder.Address2Poland, order1, true, WarehouseSeeder.Warehouse2PL);
            var delivery2 = OrdersGetSingleActionFixture.CreateDelivery(testingUser1, AddressSeeder.Address2Poland, order1, true, WarehouseSeeder.Warehouse2PL, "DEL000002");
            var deliveryProduct1 = OrdersGetSingleActionFixture.CreateDeliveryProduct(delivery1, ProductSeeder.Product11Juice.Id, 100);
            var deliveryProduct2 = OrdersGetSingleActionFixture.CreateDeliveryProduct(delivery2, ProductSeeder.Product13Soup.Id, 50);

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

            toolkit.UpdateUserInfo(OrdersGetSingleActionFixture.GetCurrentUserInfo(testingAuth0User1));

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
        public async Task OrdersGetSingleActionTest_GetSingleNotFoundOrderId_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<OrdersGetSingleAction>();

            //Arrange
            var testingUser1 = OrdersGetSingleActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = OrdersGetSingleActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = OrdersGetSingleActionFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(OrdersGetSingleActionFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(toolkit.Hashids.EncodeLong(1500)));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}