using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.CreateOrderActions.CreateOrderDeleteAddress;
using TestsBase;

namespace OrderTests.Actions.CreateOrder
{
    [TestClass()]
    public class CreateOrderDeleteAddressTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task CreateOrderDeleteAddressTest_DeleteAddresses_ReturnResponseWithDeletedAddress()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderDeleteAddressAction>();

            // Arrange 
            var testingUser1 = CreateOrderDeleteAddressFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderDeleteAddressFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = CreateOrderDeleteAddressFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            var newAddress1 = CreateOrderDeleteAddressFixture.CreateAddress(AddressSeeder.Address1Poland, AddressSeeder.Address9France);
            var newAddress2 = CreateOrderDeleteAddressFixture.CreateAddress(AddressSeeder.Address5Germany, AddressSeeder.Address6Hungary);

            var newUserAddress1 = CreateOrderDeleteAddressFixture.CreateUserAddress(testingUser1, newAddress1);
            var newUserAddress2 = CreateOrderDeleteAddressFixture.CreateUserAddress(testingUser1, newAddress2);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);
            context.Add(newAddress1);
            context.Add(newAddress2);
            context.Add(newUserAddress1);
            context.Add(newUserAddress2);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderDeleteAddressFixture.GetCurrentUserInfo(testingAuth0User1));

            var encodedAddress2Id = toolkit.Hashids.EncodeLong(newUserAddress2.Address!.Id);
            // Act
            var executed = await action.Execute(encodedAddress2Id);
            var result = executed.Value as CreateOrderDeleteAddressResponse;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newUserAddress2.Address!.Street, result.Street);
            Assert.AreEqual(1, context.UserAddresses.Where(c => c.UserId == testingUser1.Id && c.IsDeletedByUser).Count());
        }

        [TestMethod()]
        public async Task CreateOrderDeleteAddressTest_NoAddressById_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderDeleteAddressAction>();

            // Arrange 
            var testingUser1 = CreateOrderDeleteAddressFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderDeleteAddressFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = CreateOrderDeleteAddressFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderDeleteAddressFixture.GetCurrentUserInfo(testingAuth0User1));

            var encodedAddress2Id = toolkit.Hashids.EncodeLong(1500);
            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(encodedAddress2Id));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}