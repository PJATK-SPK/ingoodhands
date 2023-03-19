using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.CreateOrderActions.CreateOrderCreateOrder;
using TestsBase;

namespace OrdersTests.Actions.CreateOrder
{
    [TestClass()]
    public class CreateOrderCreateOrderTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task CreateOrderCreateOrderTest_CreateOrder_ReturnOrderName()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderCreateOrderAction>();

            // Arrange
            var testingUser1 = CreateOrderCreateOrderFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderCreateOrderFixture.CreateAuth0User(testingUser1, 1);
            var testUserROle1 = CreateOrderCreateOrderFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserROle1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderCreateOrderFixture.GetCurrentUserInfo(testingAuth0User1));

            var product1 = new CreateOrderCreateOrderProductPayload
            {
                Id = toolkit.Hashids.EncodeLong(1),
                Quantity = 5
            };
            var product2 = new CreateOrderCreateOrderProductPayload
            {
                Id = toolkit.Hashids.EncodeLong(2),
                Quantity = 10
            };
            var product3 = new CreateOrderCreateOrderProductPayload
            {
                Id = toolkit.Hashids.EncodeLong(3),
                Quantity = 2
            };

            var orderPayload = new CreateOrderCreateOrderPayload
            {
                AddressId = toolkit.Hashids.EncodeLong(4),
                Products = new List<CreateOrderCreateOrderProductPayload>
                {
                    product1,
                    product2,
                    product3
                }
            };

            // Act
            var executed = await action.Execute(orderPayload);
            var result = executed.Value as CreateOrderCreateOrderResponse;

            // Assert
            Assert.AreEqual("ORD000001", result!.OrderName);
        }
    }
}
