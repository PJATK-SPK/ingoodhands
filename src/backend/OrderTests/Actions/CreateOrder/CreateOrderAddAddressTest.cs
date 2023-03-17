using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Setup.Enums;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddress;
using TestsBase;

namespace OrderTests.Actions.CreateOrder
{
    [TestClass()]
    public class CreateOrderAddAddressTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task CreateOrderAddAddressTest_AddAddress_AddsAddressToDatabase()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderAddAddressAction>();

            // Arrange 
            var country = "Ukraine";

            var testingUser1 = CreateOrderAddAddressFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderAddAddressFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = CreateOrderAddAddressFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);
            var newPayload = CreateOrderAddAddressFixture.CreatePayload(country);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderAddAddressFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute(newPayload);
            var result = executed.Value as CreateOrderAddAddressResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.UserAddresses.Where(c => c.AddressId == toolkit.Hashids.DecodeSingleLong(result.Id)).Count());
            Assert.AreEqual(1, context.Addresses.Where(c => c.Id == toolkit.Hashids.DecodeSingleLong(result.Id)).Count());
        }

        [TestMethod()]
        public async Task CreateOrderAddAddressTest_AddAddressPayloadNullStreetValues_AddsAddressToDatabase()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderAddAddressAction>();

            // Arrange 
            var country = "Poland";
            var testingUser1 = CreateOrderAddAddressFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderAddAddressFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = CreateOrderAddAddressFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);
            var newPayload = CreateOrderAddAddressFixture.CreatePayloadNullStreetValues(country);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderAddAddressFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute(newPayload);
            var result = executed.Value as CreateOrderAddAddressResponse;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.UserAddresses.Where(c => c.AddressId == toolkit.Hashids.DecodeSingleLong(result.Id)).Count());
            Assert.AreEqual(1, context.Addresses.Where(c => c.Id == toolkit.Hashids.DecodeSingleLong(result.Id)).Count());
        }

        [TestMethod()]
        public async Task CreateOrderAddAddressTest_EmptyPayload_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderAddAddressAction>();

            // Arrange 
            var emptyPayload = new CreateOrderAddAddressPayload();

            var testingUser1 = CreateOrderAddAddressFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderAddAddressFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = CreateOrderAddAddressFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderAddAddressFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ValidationException>(() => action.Execute(emptyPayload));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ValidationException));
            Assert.IsNotNull(exception.Message);
        }


        [TestMethod()]
        public async Task CreateOrderAddAddressTest_NoCountry_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderAddAddressAction>();

            // Arrange 
            var newPayload = CreateOrderAddAddressFixture.CreatePayload("HeheLand");

            var testingUser1 = CreateOrderAddAddressFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderAddAddressFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = CreateOrderDeleteAddressFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(CreateOrderAddAddressFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(newPayload));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}
