using Autofac;
using Core;
using Core.Database;
using Core.Exceptions;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddress;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddresses;
using OrdersTests.Actions.RequestHelpActionTest;
using TestsBase;

namespace OrdersTests.Actions.CreateOrderActionTest.CreateOrderAddAddressActionTest
{
    [TestClass()]
    public class CreateOrderAddAddressActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task CreateOrderAddAddressActionTest_AddAddress_AddsAddressToDatabase()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderAddAddressAction>();

            // Arrange 
            var country = "Ukraine";

            var testingUser1 = RequestHelpGetMapActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = RequestHelpGetMapActionFixture.CreateAuth0User(testingUser1, 1);
            var newPayload = CreateOrderAddAddressActionFixture.CreatePayload(country);

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

            // Act
            var executed = await action.Execute(newPayload);
            var result = executed.Value as CreateOrderAddAddressResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.UserAddresses.Where(c => c.AddressId == toolkit.Hashids.DecodeSingleLong(result.Id)).Count());
            Assert.AreEqual(1, context.Addresses.Where(c => c.Id == toolkit.Hashids.DecodeSingleLong(result.Id)).Count());
        }

        [TestMethod()]
        public async Task CreateOrderAddAddressActionTest_AddAddressPayloadNullStreetValues_AddsAddressToDatabase()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderAddAddressAction>();

            // Arrange 
            var country = "Poland";
            var testingUser1 = RequestHelpGetMapActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = RequestHelpGetMapActionFixture.CreateAuth0User(testingUser1, 1);
            var newPayload = CreateOrderAddAddressActionFixture.CreatePayloadNullStreetValues(country);

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

            // Act
            var executed = await action.Execute(newPayload);
            var result = executed.Value as CreateOrderAddAddressResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.UserAddresses.Where(c => c.AddressId == toolkit.Hashids.DecodeSingleLong(result.Id)).Count());
            Assert.AreEqual(1, context.Addresses.Where(c => c.Id == toolkit.Hashids.DecodeSingleLong(result.Id)).Count());
        }

        [TestMethod()]
        public async Task CreateOrderAddAddressActionTest_EmptyPayload_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderAddAddressAction>();

            // Arrange 
            var emptyPayload = new CreateOrderAddAddressPayload();

            var testingUser1 = RequestHelpGetMapActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = RequestHelpGetMapActionFixture.CreateAuth0User(testingUser1, 1);
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

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ClientInputErrorException>(() => action.Execute(emptyPayload));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ClientInputErrorException));
            Assert.IsNotNull(exception.Message);
        }


        [TestMethod()]
        public async Task CreateOrderAddAddressActionTest_NoCountry_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderAddAddressAction>();

            // Arrange 
            var newPayload = CreateOrderAddAddressActionFixture.CreatePayload("HeheLand");

            var testingUser1 = RequestHelpGetMapActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = RequestHelpGetMapActionFixture.CreateAuth0User(testingUser1, 1);
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

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(newPayload));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}
