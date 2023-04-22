using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.OrdersActions.OrdersSetAsDelivered;
using TestsBase;

namespace OrdersTests.Actions.Orders
{
    [TestClass()]
    public class OrdersSetAsDeliveredTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task OrdersSetAsDeliveredTest_SetAsDeliveredAction_ReturnsOkMessage()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<OrdersSetAsDeliveredAction>();

            //Arrange
            var testingUser1 = OrdersSetAsDeliveredFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = OrdersSetAsDeliveredFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = OrdersSetAsDeliveredFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            var order1 = OrdersSetAsDeliveredFixture.CreateOrder(testingUser1, AddressSeeder.Address1Poland, "ORD000001");
            var orderProduct1 = OrdersSetAsDeliveredFixture.CreateOrderProduct(order1, ProductSeeder.Product11Juice.Id, 100);
            var orderProduct2 = OrdersSetAsDeliveredFixture.CreateOrderProduct(order1, ProductSeeder.Product13Soup.Id, 50);

            var delivery1 = OrdersSetAsDeliveredFixture.CreateDelivery(testingUser1, AddressSeeder.Address2Poland, order1, false, WarehouseSeeder.Warehouse2PL);
            var delivery2 = OrdersSetAsDeliveredFixture.CreateDelivery(testingUser1, AddressSeeder.Address2Poland, order1, false, WarehouseSeeder.Warehouse2PL, "DEL000002");
            var deliveryProduct1 = OrdersSetAsDeliveredFixture.CreateDeliveryProduct(delivery1, ProductSeeder.Product11Juice.Id, 100);
            var deliveryProduct2 = OrdersSetAsDeliveredFixture.CreateDeliveryProduct(delivery2, ProductSeeder.Product13Soup.Id, 50);

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

            Assert.IsTrue(context.Deliveries.All(c => c.IsDelivered == false));

            // Act
            var result1 = await action.Execute(toolkit.Hashids.EncodeLong(order1.Id), toolkit.Hashids.EncodeLong(delivery1.Id));
            var result2 = await action.Execute(toolkit.Hashids.EncodeLong(order1.Id), toolkit.Hashids.EncodeLong(delivery2.Id));

            // Assert            
            Assert.IsTrue(context.Deliveries.All(c => c.IsDelivered == true));
        }
    }
}
