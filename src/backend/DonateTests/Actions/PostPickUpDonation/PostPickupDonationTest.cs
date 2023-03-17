using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Donate;
using Donate.Actions.PickUpDonation.PostPickUpDonation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace DonateTests.Actions.PostPickUpDonation
{
    [TestClass()]
    public class PostPickupDonationTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new DonateModule(),
        };

        [TestMethod()]
        public async Task PerformDonateTest_PickupDonation_ChangeIsDeliveredToTrue()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PostPickupDonationAction>();

            // Arrange
            var donationNumber = "DNT000001";
            var user1 = PostPickupDonationFixture.CreateUser("Normal", "User", WarehouseSeeder.Warehouse1PL.Id);
            var auth0User1 = PostPickupDonationFixture.CreateAuth0User(user1, 1);
            var userRole1 = PostPickupDonationFixture.CreateUserRole(user1, RoleSeeder.Role4WarehouseKeeper.Id);
            var donation1 = PostPickupDonationFixture.CreateDonation(false, donationNumber);
            var webPush = PostPickupDonationFixture.CreateUserWebPush(user1, "a", "b", "c");

            context.Add(user1);
            context.Add(auth0User1);
            context.Add(userRole1);
            context.Add(donation1);
            context.Add(webPush);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = auth0User1.Email,
                EmailVerified = true,
                Identifier = auth0User1.Identifier,
                GivenName = auth0User1.FirstName,
                FamilyName = auth0User1.LastName,
                Locale = "pl",
                Name = auth0User1.FirstName + auth0User1.LastName,
                Nickname = auth0User1.Nickname,
                UpdatedAt = DateTime.UtcNow,
            });

            // Act
            await action.Execute(donationNumber);

            // Assert
            Assert.IsTrue(context.Donations.FirstOrDefault(c => c.IsDelivered == true)!.IsDelivered);
        }

        [TestMethod()]
        public async Task PerformDonateTest_PickupDonation_NoWarehouseId_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PostPickupDonationAction>();

            // Arrange
            var donationNumber = "DNT000001";
            var user1 = PostPickupDonationFixture.CreateUser("Normal", "User");
            var auth0User1 = PostPickupDonationFixture.CreateAuth0User(user1, 1);
            var userRole1 = PostPickupDonationFixture.CreateUserRole(user1, RoleSeeder.Role4WarehouseKeeper.Id);
            var donation1 = PostPickupDonationFixture.CreateDonation(false, donationNumber);

            context.Add(user1);
            context.Add(auth0User1);
            context.Add(userRole1);
            context.Add(donation1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = auth0User1.Email,
                EmailVerified = true,
                Identifier = auth0User1.Identifier,
                GivenName = auth0User1.FirstName,
                FamilyName = auth0User1.LastName,
                Locale = "pl",
                Name = auth0User1.FirstName + auth0User1.LastName,
                Nickname = auth0User1.Nickname,
                UpdatedAt = DateTime.UtcNow,
            });

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ApplicationErrorException>(() => action.Execute(donationNumber));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ApplicationErrorException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task PerformDonateTest_PickupDonation_NoDonationName_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PostPickupDonationAction>();

            // Arrange
            var notFoundDonationNumber = "HEH000009";
            var user1 = PostPickupDonationFixture.CreateUser("Normal", "User", WarehouseSeeder.Warehouse1PL.Id);
            var auth0User1 = PostPickupDonationFixture.CreateAuth0User(user1, 1);
            var userRole1 = PostPickupDonationFixture.CreateUserRole(user1, RoleSeeder.Role4WarehouseKeeper.Id);
            var donation1 = PostPickupDonationFixture.CreateDonation(false, "DNT000001");

            context.Add(user1);
            context.Add(auth0User1);
            context.Add(userRole1);
            context.Add(donation1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = auth0User1.Email,
                EmailVerified = true,
                Identifier = auth0User1.Identifier,
                GivenName = auth0User1.FirstName,
                FamilyName = auth0User1.LastName,
                Locale = "pl",
                Name = auth0User1.FirstName + auth0User1.LastName,
                Nickname = auth0User1.Nickname,
                UpdatedAt = DateTime.UtcNow,
            });

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(notFoundDonationNumber));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task PerformDonateTest_PickupDonation_IsDeliveredTrue_ThrowException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PostPickupDonationAction>();

            // Arrange
            var donationNumber = "DNT000001";
            var user1 = PostPickupDonationFixture.CreateUser("Normal", "User", WarehouseSeeder.Warehouse1PL.Id);
            var auth0User1 = PostPickupDonationFixture.CreateAuth0User(user1, 1);
            var userRole1 = PostPickupDonationFixture.CreateUserRole(user1, RoleSeeder.Role4WarehouseKeeper.Id);
            var donation1 = PostPickupDonationFixture.CreateDonation(true, donationNumber);

            context.Add(user1);
            context.Add(auth0User1);
            context.Add(userRole1);
            context.Add(donation1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = auth0User1.Email,
                EmailVerified = true,
                Identifier = auth0User1.Identifier,
                GivenName = auth0User1.FirstName,
                FamilyName = auth0User1.LastName,
                Locale = "pl",
                Name = auth0User1.FirstName + auth0User1.LastName,
                Nickname = auth0User1.Nickname,
                UpdatedAt = DateTime.UtcNow,
            });

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ClientInputErrorException>(() => action.Execute(donationNumber));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ClientInputErrorException));
            Assert.IsNotNull(exception.Message);
        }
    }
}