using Core.Database;
using Core.Setup.Enums;
using Core;
using Donate.Actions.DonateForm.GetProducts;
using Donate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsBase;
using Donate.Actions.MyDonations.GetDetails;
using Autofac;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Setup.Auth0;
using Core.Database.Models.Core;
using Core.Database.Seeders;

namespace DonateTests.Services.GetMyDonationDetailsActionTest
{
    [TestClass()]
    public class GetMyDonationDetailsActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new DonateModule(),
        };

        [TestMethod()]
        public async Task GetProductsActionTest_GeProducts_ReturnsProducts()
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
                WarehouseId = 1,
                Name = "DNT000001",
                IsExpired = false,
                IsDelivered = false,
                IsIncludedInStock = false,
                UpdateUserId = UserSeeder.ServierUser.Id,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                Status = DbEntityStatus.Active
            };
            context.Add(donation);

            var donationProduct1 = new DonationProduct
            {
                Donation = donation,
                ProductId = 1,
                Quantity = 1,
                UpdateUserId = UserSeeder.ServierUser.Id,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                Status = DbEntityStatus.Active
            };
            context.Add(donationProduct1);

            var donationProduct2 = new DonationProduct
            {
                Donation = donation,
                ProductId = 2,
                Quantity = 10,
                UpdateUserId = UserSeeder.ServierUser.Id,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                Status = DbEntityStatus.Active
            };
            context.Add(donationProduct2);

            await context.SaveChangesAsync();

            // Act
            var executed = await action.Execute(donationId);
            var result = executed.Value as GetMyDonationDetailsResponse;

            // Assert
            Assert.AreEqual("DNT000001", result.Name);
            Assert.AreEqual(2, result.Products.Count);
        }
    }
}
