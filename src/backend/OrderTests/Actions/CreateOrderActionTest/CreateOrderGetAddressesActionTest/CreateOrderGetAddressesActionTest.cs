﻿using Autofac;
using Core;
using Core.Database;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.CreateOrderActions.CreateOrderGetAddresses;
using OrdersTests.Actions.CreateOrderActionTest.CreateOrderAddAddressActionTest;
using TestsBase;

namespace OrderTests.Actions.CreateOrderActionTest.CreateOrderGetAddressesActionTest
{
    [TestClass()]
    public class CreateOrderGetAddressesActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task CreateOrderGetAddressesActionTest_GetAddresses_ReturnListOfActiveAddresses()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderGetAddressesAction>();

            // Arrange 
            var testingUser1 = CreateOrderAddAddressActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = CreateOrderAddAddressActionFixture.CreateAuth0User(testingUser1, 1);

            var newAddress1 = CreateOrderGetAddressesActionFixture.CreateAddress(AddressSeeder.Address1Poland, AddressSeeder.Address9France);
            var newAddress2 = CreateOrderGetAddressesActionFixture.CreateAddress(AddressSeeder.Address5Germany, AddressSeeder.Address6Hungary);

            var newUserAddress1 = CreateOrderGetAddressesActionFixture.CreateUserAddress(testingUser1, newAddress1);
            var newUserAddress2 = CreateOrderGetAddressesActionFixture.CreateUserAddress(testingUser1, newAddress2);

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
