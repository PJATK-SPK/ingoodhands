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

namespace CoreTests.Services.Role
{
    [TestClass()]
    public class RoleServiceTest
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
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser);

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
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser);

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
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser);

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

            // Act
            var hasRole = await action.HasRole(RoleName.Administrator, testingUser.Id);

            // Assert
            Assert.IsFalse(hasRole);
        }

        [TestMethod()]
        public async Task AssertRoleToUser_UserHasId_AddsRoleToDb()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

            // Arrange  
            var roleId = context.Roles.First(c => c.Name == RoleName.WarehouseKeeper).Id;

            var testingUser = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser);

            await context.SaveChangesAsync();

            // Act
            await action.AssertRoleToUser(RoleName.WarehouseKeeper, testingUser.Id);

            // Assert
            Assert.AreEqual(roleId, context.UserRoles.First(c => c.UserId == testingUser.Id).RoleId);
        }

        [TestMethod()]
        public async Task AssertRoleToUser_UserWithoutId_AddsRoleToDb()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

            // Arrange
            var roleId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;

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
            await action.AssertRoleToUser(RoleName.Administrator);

            // Assert
            Assert.AreEqual(roleId, context.UserRoles.First(c => c.UserId == testingUser.Id).RoleId);
        }

        [TestMethod()]
        public async Task AssertRoleToUser_UserWithoutId_ThrowsExcetpion()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

            // Arrange  
            var roleId = context.Roles.First(c => c.Name == RoleName.WarehouseKeeper).Id;

            var testingUser = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser);

            var testUserRole = new UserRole
            {
                RoleId = roleId,
                User = testingUser,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUserRole);

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
            var exception = await Assert.ThrowsExceptionAsync<UnauthorizedException>(() => action.AssertRoleToUser(RoleName.WarehouseKeeper));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(UnauthorizedException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task AssertRoleToUser_UserHasId_ThrowsExcetpion()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

            // Arrange  
            var roleId = context.Roles.First(c => c.Name == RoleName.WarehouseKeeper).Id;

            var testingUser = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser);

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

            // Act
            var exception = await Assert.ThrowsExceptionAsync<UnauthorizedException>(() => action.AssertRoleToUser(RoleName.WarehouseKeeper, testingUser.Id));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(UnauthorizedException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task ThrowIfNoRole_UserHasId_DoesntHaveRole_ThrowsExcetpion()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

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

            // Act
            var exception = await Assert.ThrowsExceptionAsync<UnauthorizedException>(() => action.ThrowIfNoRole(RoleName.WarehouseKeeper, testingUser.Id));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(UnauthorizedException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task ThrowIfNoRole_UserWithoutId_DoesntHaveRole_ThrowsExcetpion()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

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

            var testUserRole = new UserRole
            {
                RoleId = roleId,
                User = testingUser,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            context.Add(testUserRole);

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
            var exception = await Assert.ThrowsExceptionAsync<UnauthorizedException>(() => action.ThrowIfNoRole(RoleName.WarehouseKeeper));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(UnauthorizedException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task ThrowIfNoRole_UserHasId_HasRole_DoesntThrowsExcetpion()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<RoleService>();

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

            // Act
            await action.ThrowIfNoRole(RoleName.Donor, testingUser.Id);

            // Assert
            Assert.IsTrue(true);
        }
    }
}

