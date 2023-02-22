using Autofac;
using Core;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Dynamic.Core;
using TestsBase;

namespace CoreTests.Services
{
    [TestClass()]
    public class RoleServiceTests
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None)
        };

        [TestMethod()]
        public async Task GetRolesAsync_NoUserId_ReturnsCurrentUserRoles()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

            // Arrange
            var roleId = context.Roles.First(c => c.Name == RoleName.WarehouseKeeper).Id;

            var testingUser = new User()
            {
                Id = 2,
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
                UserId = 2,
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

            // Act
            var roles = await action.GetRolesAsync();

            // Assert
            Assert.AreEqual(1, roles.Count);
            Assert.AreEqual(RoleName.WarehouseKeeper, roles.First(c => c == RoleName.WarehouseKeeper));

        }

        [TestMethod()]
        public async Task GetRolesAsync_WithUserId_ReturnsUserRoles()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

            // Arrange
            var roleId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;

            var testingUser = new User()
            {
                Id = 2,
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
            };
            context.Add(testingUser);

            var testUserRole = new UserRole
            {
                RoleId = roleId,
                UserId = 2,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUserRole);
            await context.SaveChangesAsync();
            // Act
            var roles = await action.GetRolesAsync(testingUser.Id);

            // Assert
            Assert.AreEqual(1, roles.Count);
            Assert.AreEqual(RoleName.Administrator, roles.First(c => c == RoleName.Administrator));
        }


        [TestMethod()]
        public async Task GetRolesAsync_NoUser_NoUserId_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();
            // Arrange

            var testId = 100;

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ApplicationErrorException>(() => action.GetRolesAsync(testId));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ApplicationErrorException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task HasRole_UserHasRole_ReturnsTrue()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

            // Arrange
            var roleId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;

            var testingUser = new User()
            {
                Id = 2,
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
            };
            context.Add(testingUser);

            var testUserRole = new UserRole
            {
                RoleId = roleId,
                UserId = 2,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUserRole);
            await context.SaveChangesAsync();

            // Act
            var hasRole = await action.HasRole(RoleName.Administrator, testingUser.Id);

            // Assert
            Assert.IsTrue(hasRole);
        }

        [TestMethod()]
        public async Task HasRole_UserDoesNotHaveRole_ShouldReturnFalse()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

            // Arrange
            var roleId = context.Roles.First(c => c.Name == RoleName.WarehouseKeeper).Id;

            var testingUser = new User()
            {
                Id = 2,
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
            };
            context.Add(testingUser);

            var testUserRole = new UserRole
            {
                RoleId = roleId,
                UserId = 2,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUserRole);
            await context.SaveChangesAsync();

            // Act
            var hasRole = await action.HasRole(RoleName.Administrator, testingUser.Id);

            // Assert
            Assert.IsFalse(hasRole);
        }


        [TestMethod()]
        public async Task AssertRole_UserHasId()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

            // Arrange  
            var roleId = context.Roles.First(c => c.Name == RoleName.WarehouseKeeper).Id;

            var testingUser = new User()
            {
                Id = 2,
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
            };
            context.Add(testingUser);

            await context.SaveChangesAsync();

            // Act
            await action.AssertRole(RoleName.WarehouseKeeper, testingUser.Id);

            // Assert
            Assert.AreEqual(roleId, context.UserRoles.First(c => c.UserId == testingUser.Id).RoleId);
        }

        [TestMethod()]
        public async Task AssertRole_UserWithoutId()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

            // Arrange
            var roleId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;

            var testingUser = new User()
            {
                Id = 2,
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

            // Act
            await action.AssertRole(RoleName.Administrator);

            // Assert
            Assert.AreEqual(roleId, context.UserRoles.First(c => c.UserId == testingUser.Id).RoleId);
        }
    }
}

