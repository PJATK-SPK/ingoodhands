using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddress;
using OrdersTests.Actions.RequestHelpActionTest;
using OrderTests.Actions.CreateOrderActionTest.CreateOrderDeleteAddressActionTest;
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

            var testingUser1 = CreateOrderAddAddressActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderAddAddressActionFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = CreateOrderAddAddressActionFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);
            var newPayload = CreateOrderAddAddressActionFixture.CreatePayload(country);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderAddAddressActionFixture.GetCurrentUserInfo(testingAuth0User1));

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
            var testingUser1 = CreateOrderAddAddressActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderAddAddressActionFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = CreateOrderAddAddressActionFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);
            var newPayload = CreateOrderAddAddressActionFixture.CreatePayloadNullStreetValues(country);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderAddAddressActionFixture.GetCurrentUserInfo(testingAuth0User1));

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

            var testingUser1 = CreateOrderAddAddressActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderAddAddressActionFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = CreateOrderAddAddressActionFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderAddAddressActionFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ValidationException>(() => action.Execute(emptyPayload));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ValidationException));
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

            var testingUser1 = CreateOrderAddAddressActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderAddAddressActionFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = CreateOrderDeleteAddressActionFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderAddAddressActionFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(newPayload));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}
