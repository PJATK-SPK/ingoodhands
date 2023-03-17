using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.CreateOrderActions.CreateOrderGetCountries;
using TestsBase;

namespace OrderTests.Actions.CreateOrder
{
    [TestClass()]
    public class CreateOrderGetCountriesTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task CreateOrderGetCountriesTest_GetCountries_ReturnsListOfCountries()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderGetCountriesAction>();

            // Arrange
            var testingUser1 = CreateOrderGetCountriesFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderGetCountriesFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = CreateOrderGetCountriesFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderGetCountriesFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute();
            var result = executed.Value as List<string>;

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsTrue(result!.Any());
            Assert.AreEqual(247, result.Count);
        }
    }
}
