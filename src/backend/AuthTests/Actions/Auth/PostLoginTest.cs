using Auth;
using Auth.Actions.AuthActions.PostLogin;
using Autofac;
using Core;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Dynamic.Core;
using TestsBase;

namespace AuthTests.Actions.Auth
{
    [TestClass()]
    public class PostLoginTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new AuthModule(),
        };

        [TestMethod()]
        public async Task PostLoginTest_UserAndAuth0UserPresent()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PostLoginAction>();

            // Arrange
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
                LastName = "Zerouser",
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
            var result = await action.Execute();

            var donorRoleId = context.Roles.Single(c => c.Name == RoleName.Donor).Id;
            var needyRoleId = context.Roles.Single(c => c.Name == RoleName.Needy).Id;

            // Assert
            Assert.AreEqual(2, context.Users.Count());
            Assert.AreEqual(1, context.Auth0Users.Count());
            Assert.IsTrue(context.UserRoles.Where(c => c.UserId == testingUser.Id && c.RoleId == donorRoleId).Any());
            Assert.IsTrue(context.UserRoles.Where(c => c.UserId == testingUser.Id && c.RoleId == needyRoleId).Any());
        }

        [TestMethod()]
        public async Task PostLoginTest_UserAndAuth0UserNotPresent()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PostLoginAction>();

            // Arrange
            var newEmail = "s21166@pjwstk.edu.pl";
            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = newEmail,
                EmailVerified = true,
                FamilyName = "Badysiak",
                GivenName = "Paweł",
                Locale = "pl",
                Name = "Paweł Badysiak",
                Nickname = "s21166",
                Identifier = "google-oauth2|117638106834834546346",
                UpdatedAt = DateTime.UtcNow,
            });

            // Act
            var result = await action.Execute();

            var donorRoleId = context.Roles.Single(c => c.Name == RoleName.Donor).Id;
            var needyRoleId = context.Roles.Single(c => c.Name == RoleName.Needy).Id;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(context.Users.Single(x => x.Email == newEmail)?.Email, newEmail);
            Assert.AreEqual(2, context.Users.Count());
            Assert.AreEqual(1, context.Auth0Users.Count());
            Assert.IsTrue(context.UserRoles.Where(c => c.User!.Email == newEmail && c.RoleId == donorRoleId).Any());
            Assert.IsTrue(context.UserRoles.Where(c => c.User!.Email == newEmail && c.RoleId == needyRoleId).Any());
        }

        [TestMethod()]
        public async Task PostLoginTest_UserYesAuth0UserNo()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PostLoginAction>();

            // Arrange
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

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = testingUser.Email,
                EmailVerified = true,
                Identifier = "google-oauth2|117638106834834546346",
                GivenName = testingUser.FirstName,
                FamilyName = testingUser.LastName,
                Locale = "pl",
                Name = testingUser.FirstName + testingUser.LastName,
                Nickname = "test",
                UpdatedAt = DateTime.UtcNow,
            });

            // Act
            var result = await action.Execute();

            var donorRoleId = context.Roles.Single(c => c.Name == RoleName.Donor).Id;
            var needyRoleId = context.Roles.Single(c => c.Name == RoleName.Needy).Id;

            // Assert
            Assert.AreEqual(context.Auth0Users.Single(x => x.Email == testingUser.Email)?.User, testingUser);
            Assert.AreEqual(2, context.Users.Count());
            Assert.AreEqual(1, context.Auth0Users.Count());
            Assert.IsTrue(context.UserRoles.Where(c => c.UserId == testingUser.Id && c.RoleId == donorRoleId).Any());
            Assert.IsTrue(context.UserRoles.Where(c => c.UserId == testingUser.Id && c.RoleId == needyRoleId).Any());
        }

        [TestMethod()]
        public async Task PostLoginTest_NoRolesInDb()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PostLoginAction>();

            // Arrange
            var testingUser = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser);
            context.Roles.RemoveRange(context.Roles.ToList());
            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = testingUser.Email,
                EmailVerified = true,
                Identifier = "google-oauth2|117638106834834546346",
                GivenName = testingUser.FirstName,
                FamilyName = testingUser.LastName,
                Locale = "pl",
                Name = testingUser.FirstName + testingUser.LastName,
                Nickname = "test",
                UpdatedAt = DateTime.UtcNow,
            });

            // Act
            await Assert.ThrowsExceptionAsync<ApplicationErrorException>(() => action.Execute());
        }

        [TestMethod()]
        public async Task PostLoginTest_NoServiceUserInDb()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PostLoginAction>();

            // Arrange
            var testingUser = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com",
                WarehouseId = null
            };
            context.Add(testingUser);
            context.Users.RemoveRange(context.Users.ToList());
            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = testingUser.Email,
                EmailVerified = true,
                Identifier = "google-oauth2|117638106834834546346",
                FamilyName = testingUser.LastName,
                Locale = "pl",
                Name = testingUser.FirstName + testingUser.LastName,
                Nickname = "test",
                UpdatedAt = DateTime.UtcNow,
            });

            // Act
            await Assert.ThrowsExceptionAsync<ApplicationErrorException>(() => action.Execute());
        }

        [TestMethod()]
        public async Task PostLoginTest_UserNoAuth0UserYes()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PostLoginAction>();

            // Arrange
            var testingAuth0User = new Auth0User()
            {
                FirstName = "Auth",
                LastName = "Zerouser",
                Nickname = "Auth0",
                UpdateUser = new User(),
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Email = "auth0@user.com",
                Identifier = "testingIdentifier",
                User = new User(),
                UserId = new User().Id
            };
            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = testingAuth0User.Email,
                EmailVerified = true,
                Identifier = testingAuth0User.Identifier,
                FamilyName = testingAuth0User.LastName,
                Locale = "pl",
                Name = testingAuth0User.FirstName + testingAuth0User.LastName,
                Nickname = testingAuth0User.Nickname,
                UpdatedAt = DateTime.UtcNow,
            });

            // Act
            var result = await action.Execute();

            // Assert
            Assert.AreEqual(context.Users.FirstOrDefault(x => x.Email == testingAuth0User.Email)?.Email, testingAuth0User.Email);
            Assert.AreEqual(2, context.Users.Count());
            Assert.AreEqual(1, context.Auth0Users.Count());
        }

        [TestMethod()]
        public async Task PostLoginTest_UserDataValidationThrowsError()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PostLoginAction>();

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = null,
                EmailVerified = false,
                Identifier = null,
                GivenName = null,
                FamilyName = null,
                Locale = "pl",
                Name = null,
                Nickname = null,
                UpdatedAt = DateTime.UtcNow,
            });

            //Act 
            var exception = await Assert.ThrowsExceptionAsync<ValidationException>(() => action.Execute());

            //Assert
            Assert.IsInstanceOfType(exception, typeof(ValidationException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task PostLoginTest_EmailNotVerifiedAndVerified()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PostLoginAction>();

            // Arrange
            var newEmail = "s21166@pjwstk.edu.pl";

            var currentUserInfo = new CurrentUserInfo
            {
                Email = newEmail,
                EmailVerified = false,
                FamilyName = "Badysiak",
                GivenName = "Paweł",
                Locale = "pl",
                Name = "Paweł Badysiak",
                Nickname = "s21166",
                Identifier = "google-oauth2|117638106834834546346",
                UpdatedAt = DateTime.UtcNow,
            };

            toolkit.UpdateUserInfo(currentUserInfo);

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ValidationException>(() => action.Execute());

            currentUserInfo.EmailVerified = true;
            toolkit.UpdateUserInfo(currentUserInfo);

            var result = await action.Execute();

            var donorRoleId = context.Roles.Single(c => c.Name == RoleName.Donor).Id;
            var needyRoleId = context.Roles.Single(c => c.Name == RoleName.Needy).Id;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(context.Users.Single(x => x.Email == newEmail)?.Email, newEmail);
            Assert.AreEqual(2, context.Users.Count());
            Assert.AreEqual(1, context.Auth0Users.Count());
            Assert.IsTrue(context.UserRoles.Where(c => c.User!.Email == newEmail && c.RoleId == donorRoleId).Any());
            Assert.IsTrue(context.UserRoles.Where(c => c.User!.Email == newEmail && c.RoleId == needyRoleId).Any());
        }
    }
}