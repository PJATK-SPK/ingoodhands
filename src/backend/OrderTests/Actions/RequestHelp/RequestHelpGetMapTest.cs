using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.RequestHelpActions.RequestHelpGetMap;
using OrderTests.Actions.RequestHelp;
using System.Linq.Dynamic.Core;
using TestsBase;

namespace OrdersTests.Actions.RequestHelp
{
    [TestClass()]
    public class RequestHelpGetMapTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task RequestHelpGetMapTest_GetMapAction_ReturnsResponse()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RequestHelpGetMapAction>();

            //Arrange
            var testingUser1 = RequestHelpGetMapFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = RequestHelpGetMapFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = RequestHelpGetMapFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            var order1 = RequestHelpGetMapFixture.CreateOrder(testingUser1, AddressSeeder.Address1Poland, "ORD000001");
            var orderProduct1 = RequestHelpGetMapFixture.CreateOrderProduct(order1, ProductSeeder.Product11Juice.Id, 100);
            var orderProduct2 = RequestHelpGetMapFixture.CreateOrderProduct(order1, ProductSeeder.Product13Soup.Id, 50);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            context.Add(order1);
            context.Add(orderProduct1);
            context.Add(orderProduct2);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = testingAuth0User1.Email,
                EmailVerified = true,
                Identifier = testingAuth0User1.Identifier,
                GivenName = testingAuth0User1.FirstName,
                FamilyName = testingAuth0User1.LastName,
                Locale = "pl",
                Name = testingAuth0User1.FirstName + testingAuth0User1.LastName,
                Nickname = testingAuth0User1.Nickname,
                UpdatedAt = DateTime.UtcNow,
            });

            // Act
            var executed = await action.Execute();
            var result = executed.Value as RequestHelpGetMapResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsTrue(result!.Warehouses.Any());
            Assert.IsTrue(result!.Orders.Any());
            Assert.AreEqual(1, result!.Orders.Count);
            Assert.AreEqual(9, result!.Warehouses.Count);
        }
    }
}