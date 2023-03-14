using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.CreateOrderActions.CreateOrderDeleteAddress;
using TestsBase;

namespace OrderTests.Actions.CreateOrderActionTest.CreateOrderDeleteAddressActionTest
{
    [TestClass()]
    public class CreateOrderDeleteAddressActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task CreateOrderDeleteAddressActionTest_DeleteAddresses_ReturnResponseWithDeletedAddress()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderDeleteAddressAction>();

            // Arrange 
            var testingUser1 = CreateOrderDeleteAddressActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderDeleteAddressActionFixture.CreateAuth0User(testingUser1, 1);

            var newAddress1 = CreateOrderDeleteAddressActionFixture.CreateAddress(AddressSeeder.Address1Poland, AddressSeeder.Address9France);
            var newAddress2 = CreateOrderDeleteAddressActionFixture.CreateAddress(AddressSeeder.Address5Germany, AddressSeeder.Address6Hungary);

            var newUserAddress1 = CreateOrderDeleteAddressActionFixture.CreateUserAddress(testingUser1, newAddress1);
            var newUserAddress2 = CreateOrderDeleteAddressActionFixture.CreateUserAddress(testingUser1, newAddress2);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(newAddress1);
            context.Add(newAddress2);
            context.Add(newUserAddress1);
            context.Add(newUserAddress2);

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
        public async Task CreateOrderDeleteAddressActionTest_NoAddressById_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderDeleteAddressAction>();

            // Arrange 
            var testingUser1 = CreateOrderDeleteAddressActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderDeleteAddressActionFixture.CreateAuth0User(testingUser1, 1);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);

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

            var encodedAddress2Id = toolkit.Hashids.EncodeLong(1500);
            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(encodedAddress2Id));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}