using AuthService;
using AuthService.BusinessLogic.PostLogin;
using AuthService.BusinessLogic.PostLogin.Exceptions;
using Autofac;
using Core;
using Core.Auth0;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Dynamic.Core;
using TestsCore;
using static System.Net.Mime.MediaTypeNames;

namespace AuthServiceTests.BusinessLogic.PostLogin
{
    [TestClass()]
    public class PostLoginActionTests
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(false),
            new AuthServiceModule(),
        };

        [TestMethod()]
        public async Task PostLoginActionTest_UserAndAuth0UserPresent()
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
                Email = "test@testing.com"
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

            // Assert
            Assert.AreEqual(testingUser.Auth0Users?[0], testingAuth0User);
            Assert.AreEqual(testingAuth0User.User, testingUser);
            Assert.AreEqual(2, context.Users.Count());
            Assert.AreEqual(1, context.Auth0Users.Count());
        }

        [TestMethod()]
        public async Task PostLoginActionTest_UserAndAuth0UserNotPresent()
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

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(context.Users.Single(x => x.Email == newEmail)?.Email, newEmail);
            Assert.AreEqual(2, context.Users.Count());
            Assert.AreEqual(1, context.Auth0Users.Count());
        }

        [TestMethod()]
        public async Task PostLoginActionTest_UserYesAuth0UserNo()
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
                Email = "test@testing.com"
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

            // Assert
            Assert.AreEqual(context.Auth0Users.Single(x => x.Email == testingUser.Email)?.User, testingUser);
            Assert.AreEqual(2, context.Users.Count());
            Assert.AreEqual(1, context.Auth0Users.Count());
        }

        [TestMethod()]
        public async Task PostLoginActionTest_UserNoAuth0UserYes()
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
                GivenName = testingAuth0User.FirstName,
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
        public async Task PostLoginActionTest_UserDataValidationThrowsError()
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
            var exception = await Assert.ThrowsExceptionAsync<PostLoginDataCheckValidationException>(() => action.Execute());

            //Assert
            Assert.IsInstanceOfType(exception, typeof(PostLoginDataCheckValidationException));
            Assert.AreEqual("Data is invalid at: CurrentAuth0UserInfo in PostLoginAction didn't pass validation", exception.Message);
        }
    }
}