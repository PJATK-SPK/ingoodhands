using Autofac;
using Core;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Donate;
using Donate.Actions.MyDonations.GetDetails;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace DonateTests.Services.GetMyDonationDetails
{
    [TestClass()]
    public class GetMyDonationDetailsTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new DonateModule(),
        };

        [TestMethod()]
        public async Task GetMyDonationDetailsTest_GetDetails_ReturnsResponse()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetMyDonationDetailsAction>();

            // Arrange
            var donationId = toolkit.Hashids.EncodeLong(1);
            var roleId = context.Roles.First(c => c.Name == RoleName.Donor).Id;

            var testingUser = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser);

            var testingAuth0User = new Auth0User()
            {
                FirstName = "Auth",
                LastName = "Auth0User",
                Nickname = "Auth0",
                UpdateUser = testingUser,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Email = testingUser.Email,
                Identifier = "testingIdentifier",
                User = testingUser,
                UserId = testingUser.Id
            };
            context.Add(testingAuth0User);

            var testUserRole = new UserRole
            {
                RoleId = roleId,
                User = testingUser,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUserRole);

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = testingAuth0User.Email,
                EmailVerified = true,
                Identifier = testingAuth0User.Identifier,
                GivenName = testingAuth0User.FirstName,
                FamilyName = testingAuth0User.LastName,
                Locale = "pl",
                Name = testingAuth0User.FirstName + testingAuth0User.LastName,
                Nickname = testingAuth0User.Nickname,
                UpdatedAt = DateTime.UtcNow,
            });

            var donation = new Donation
            {
                Id = 1,
                CreationUserId = testingUser.Id,
                CreationUser = testingUser,
                CreationDate = DateTime.UtcNow,
                WarehouseId = WarehouseSeeder.Warehouse1PL.Id,
                Name = "DNT000001",
                IsExpired = false,
                IsDelivered = false,
                IsIncludedInStock = false,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(donation);

            var donationProduct1 = new DonationProduct
            {
                Donation = donation,
                ProductId = ProductSeeder.Product1Rice.Id,
                Quantity = 1,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(donationProduct1);

            var donationProduct2 = new DonationProduct
            {
                Donation = donation,
                ProductId = ProductSeeder.Product2Pasta.Id,
                Quantity = 10,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(donationProduct2);

            await context.SaveChangesAsync();

            // Act
            var executed = await action.Execute(donationId);
            var result = executed.Value as GetMyDonationDetailsResponse;

            // Assert
            Assert.AreEqual("DNT000001", result!.Name);
            Assert.AreEqual(2, result.Products.Count);
        }

        [TestMethod()]
        public async Task GetMyDonationDetailsTest_NoDonationInDb_ThrowException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetMyDonationDetailsAction>();

            // Arrange
            var donationId = toolkit.Hashids.EncodeLong(100);
            var roleId = context.Roles.First(c => c.Name == RoleName.Donor).Id;

            var testingUser = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser);

            var testingAuth0User = new Auth0User()
            {
                FirstName = "Auth",
                LastName = "Auth0User",
                Nickname = "Auth0",
                UpdateUser = testingUser,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Email = testingUser.Email,
                Identifier = "testingIdentifier",
                User = testingUser,
                UserId = testingUser.Id
            };
            context.Add(testingAuth0User);

            var testUserRole = new UserRole
            {
                RoleId = roleId,
                User = testingUser,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUserRole);

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = testingAuth0User.Email,
                EmailVerified = true,
                Identifier = testingAuth0User.Identifier,
                GivenName = testingAuth0User.FirstName,
                FamilyName = testingAuth0User.LastName,
                Locale = "pl",
                Name = testingAuth0User.FirstName + testingAuth0User.LastName,
                Nickname = testingAuth0User.Nickname,
                UpdatedAt = DateTime.UtcNow,
            });

            await context.SaveChangesAsync();

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(donationId));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task GetMyDonationDetailsTest_NoDonationProductInDb_ThrowException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetMyDonationDetailsAction>();

            // Arrange
            var donationId = toolkit.Hashids.EncodeLong(1);
            var roleId = context.Roles.First(c => c.Name == RoleName.Donor).Id;

            var testingUser = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser);

            var testingAuth0User = new Auth0User()
            {
                FirstName = "Auth",
                LastName = "Auth0User",
                Nickname = "Auth0",
                UpdateUser = testingUser,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Email = testingUser.Email,
                Identifier = "testingIdentifier",
                User = testingUser,
                UserId = testingUser.Id
            };
            context.Add(testingAuth0User);

            var testUserRole = new UserRole
            {
                RoleId = roleId,
                User = testingUser,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUserRole);

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = testingAuth0User.Email,
                EmailVerified = true,
                Identifier = testingAuth0User.Identifier,
                GivenName = testingAuth0User.FirstName,
                FamilyName = testingAuth0User.LastName,
                Locale = "pl",
                Name = testingAuth0User.FirstName + testingAuth0User.LastName,
                Nickname = testingAuth0User.Nickname,
                UpdatedAt = DateTime.UtcNow,
            });

            var donation = new Donation
            {
                Id = 1,
                CreationUserId = testingUser.Id,
                CreationUser = testingUser,
                CreationDate = DateTime.UtcNow,
                WarehouseId = WarehouseSeeder.Warehouse1PL.Id,
                Name = "DNT000001",
                IsExpired = false,
                IsDelivered = false,
                IsIncludedInStock = false,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(donation);

            await context.SaveChangesAsync();

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(donationId));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}
