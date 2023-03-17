﻿using Autofac;
using Core;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Donate;
using Donate.Actions.MyDonations.GetList;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Dynamic.Core;
using TestsBase;

namespace DonateTests.Services.GetListMyDonation
{
    [TestClass()]
    public class GetListMyDonationTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new DonateModule(),
        };

        [TestMethod()]
        public async Task GetListMyDonationTest_GetList_ReturnListOfDonations()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetListMyDonationAction>();

            // Arrange
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
                WarehouseId = 1,
                Name = "DNT000001",
                IsExpired = false,
                IsDelivered = false,
                IsIncludedInStock = false,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(donation);

            var donation2 = new Donation
            {
                Id = 2,
                CreationUserId = testingUser.Id,
                CreationUser = testingUser,
                CreationDate = DateTime.UtcNow,
                WarehouseId = 1,
                Name = "DNT000002",
                IsExpired = false,
                IsDelivered = false,
                IsIncludedInStock = false,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(donation2);
            await context.SaveChangesAsync();

            // Act
            var executed = await action.Execute(1, 100);
            var result = executed.Value as PagedResult<GetListMyDonationsItemResponse>;

            // Assert
            Assert.IsTrue(result!.Queryable.Any());
            Assert.AreEqual(2, result!.Queryable.Count());
        }
    }
}