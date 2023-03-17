using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.CreateOrderActions.CreateOrderGetAddresses;
using TestsBase;

namespace OrderTests.Actions.CreateOrder
{
    [TestClass()]
    public class CreateOrderGetAddressesTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task CreateOrderGetAddressesTest_GetAddresses_ReturnListOfActiveAddresses()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderGetAddressesAction>();

            // Arrange 
            var testingUser1 = CreateOrderGetAddressesFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderGetAddressesFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = CreateOrderGetAddressesFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            var newAddress1 = CreateOrderGetAddressesFixture.CreateAddress(AddressSeeder.Address1Poland, AddressSeeder.Address9France);
            var newAddress2 = CreateOrderGetAddressesFixture.CreateAddress(AddressSeeder.Address5Germany, AddressSeeder.Address6Hungary);

            var newUserAddress1 = CreateOrderGetAddressesFixture.CreateUserAddress(testingUser1, newAddress1);
            var newUserAddress2 = CreateOrderGetAddressesFixture.CreateUserAddress(testingUser1, newAddress2);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);
            context.Add(newAddress1);
            context.Add(newAddress2);
            context.Add(newUserAddress1);
            context.Add(newUserAddress2);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderGetAddressesFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute();
            var result = executed.Value as List<CreateOrderGetAddressesItemResponse>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result!.Any());
            Assert.AreEqual(2, result.Count);
        }
    }
}
