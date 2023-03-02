using Auth.Actions.AuthActions.PostLogin;
using Auth;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsBase;
using Autofac;
using Auth.Actions.AuthActions.ManageUsersActions;
using System.Linq.Dynamic.Core;

namespace AuthTests.Actions.AuthActionsTest.ManageUsersActionsTest
{
    [TestClass()]
    public class ManageUsersActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new AuthModule(),
        };

        [TestMethod()]
        public async Task ManageUsersActionTest_ReturnTwoUsersWithRoles()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<ManageUsersAction>();

            // Arrange
            var roleDonorId = context.Roles.First(c => c.Name == RoleName.Donor).Id;
            var roleAdminId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;
            var roleDelivererId = context.Roles.First(c => c.Name == RoleName.Deliverer).Id;

            var testingUser1 = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser1);

            var testingAuth0User1 = new Auth0User()
            {
                FirstName = "Auth",
                LastName = "Auth0User",
                Nickname = "Auth0",
                UpdateUser = testingUser1,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Email = testingUser1.Email,
                Identifier = "testingIdentifier",
                User = testingUser1,
                UserId = testingUser1.Id
            };
            context.Add(testingAuth0User1);

            var testUser1Role = new UserRole
            {
                RoleId = roleDonorId,
                User = testingUser1,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUser1Role);

            var testUser1Role2 = new UserRole
            {
                RoleId = roleAdminId,
                User = testingUser1,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUser1Role2);

            var testUser1Role3 = new UserRole
            {
                RoleId = roleDelivererId,
                User = testingUser1,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUser1Role3);

            //Second User
            var testingUser2 = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User2",
                Email = "test2@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser2);

            var testingAuth0User2 = new Auth0User()
            {
                FirstName = "Auth",
                LastName = "Auth0User2",
                Nickname = "Auth02",
                UpdateUser = testingUser2,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Email = testingUser2.Email,
                Identifier = "testingIdentifier",
                User = testingUser2,
                UserId = testingUser2.Id
            };
            context.Add(testingAuth0User1);

            var testUser2Role = new UserRole
            {
                RoleId = roleDonorId,
                User = testingUser2,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUser2Role);
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
            var executed = await action.Execute(1, 100);
            var result = executed.Value as PagedResult<ManageUsersResponseItem>;

            // Assert
            Assert.IsTrue(result!.Queryable.Any());
            Assert.AreEqual(3, result!.Queryable.Count());
        }

        [TestMethod()]
        public async Task ManageUsersActionTest_ReturnFilteredUser()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<ManageUsersAction>();

            // Arrange
            var roleDonorId = context.Roles.First(c => c.Name == RoleName.Donor).Id;
            var roleAdminId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;
            var roleDelivererId = context.Roles.First(c => c.Name == RoleName.Deliverer).Id;

            var testingUser1 = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser1);

            var testingAuth0User1 = new Auth0User()
            {
                FirstName = "Auth",
                LastName = "Auth0User",
                Nickname = "Auth0",
                UpdateUser = testingUser1,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Email = testingUser1.Email,
                Identifier = "testingIdentifier",
                User = testingUser1,
                UserId = testingUser1.Id
            };
            context.Add(testingAuth0User1);

            var testUser1Role = new UserRole
            {
                RoleId = roleDonorId,
                User = testingUser1,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUser1Role);

            var testUser1Role2 = new UserRole
            {
                RoleId = roleAdminId,
                User = testingUser1,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUser1Role2);

            var testUser1Role3 = new UserRole
            {
                RoleId = roleDelivererId,
                User = testingUser1,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUser1Role3);

            //Second User
            var testingUser2 = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User2",
                Email = "test2@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser2);

            var testingAuth0User2 = new Auth0User()
            {
                FirstName = "Auth",
                LastName = "Auth0User2",
                Nickname = "Auth02",
                UpdateUser = testingUser2,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Email = testingUser2.Email,
                Identifier = "testingIdentifier",
                User = testingUser2,
                UserId = testingUser2.Id
            };
            context.Add(testingAuth0User1);

            var testUser2Role = new UserRole
            {
                RoleId = roleDonorId,
                User = testingUser2,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUser2Role);
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
            var executed = await action.Execute(1, 100, "Normal User");
            var result = executed.Value as PagedResult<ManageUsersResponseItem>;

            // Assert
            Assert.IsTrue(result!.Queryable.Any());
            Assert.AreEqual(2, result!.Queryable.Count());
        }
    }
}
