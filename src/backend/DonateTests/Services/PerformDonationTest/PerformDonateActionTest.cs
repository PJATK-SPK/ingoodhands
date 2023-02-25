using Autofac;
using Core;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Donate;
using Donate.Actions.DonateForm.PerformDonate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestsBase;

namespace DonateTests.Services.PerformDonationTest
{
    [TestClass()]
    public class PerformDonateActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new DonateModule(),
        };

        [TestMethod()]
        public async Task PerformDonateActionTest_PerformDonation_ReturnDonationName()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PerformDonateAction>();

            // Arrange
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
            await context.SaveChangesAsync();

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

            var product1 = new PerformDonateProductPayload
            {
                Id = toolkit.Hashids.EncodeLong(1),
                Quantity = 5
            };
            var product2 = new PerformDonateProductPayload
            {
                Id = toolkit.Hashids.EncodeLong(2),
                Quantity = 10
            };
            var product3 = new PerformDonateProductPayload
            {
                Id = toolkit.Hashids.EncodeLong(3),
                Quantity = 2
            };

            // Sample PerformDonatePayload instance with the above products
            var donatePayload = new PerformDonatePayload
            {
                WarehouseId = toolkit.Hashids.EncodeLong(4),
                Products = new List<PerformDonateProductPayload>
                {
                    product1,
                    product2,
                    product3
                }
            };

            // Act
            var executed = await action.Execute(donatePayload);
            var result = executed.Value as PerformDonateResponse;

            // Assert
            Assert.AreEqual("DNT000001", result!.DonateName);
        }
    }
}
