using Auth;
using Auth.Actions.UserSettingsActions.GetAuth0UsersByCurrentUser;
using Autofac;
using Core;
using Core.Setup.Auth0;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Dynamic.Core;
using TestsBase;
using Core.Setup.Enums;

namespace AuthTests.Actions.UserSettingsTests.GetAuth0UsersByCurrentUser
{
    [TestClass()]
    public class GetAuth0UsersByCurrentUserActionTests
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new AuthModule(),
        };

        [TestMethod()]
        public async Task GetAuth0UsersByCurrentUserActionTests_GetAllAutho0UsersFromUser()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetAuth0UsersByCurrentUserAction>();

            // Arrange
            var testingUser = new User()
            {
                Status = DbEntityStatus.Active,
                FirstName = "Normal",
                LastName = "User",
                Email = "test@testing.com"
            };
            context.Add(testingUser);

            var testingAuth0UserOne = new Auth0User()
            {
                FirstName = "Auth",
                LastName = "Zerouser",
                Nickname = "Auth0One",
                UpdateUser = testingUser,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Email = testingUser.Email,
                Identifier = "testingIdentifierOne",
                User = testingUser,
                UserId = testingUser.Id
            };
            context.Add(testingAuth0UserOne);

            var testingAuth0UserTwo = new Auth0User()
            {
                FirstName = "AuthTwo",
                LastName = "AnotherUser",
                Nickname = "Auth0Two",
                UpdateUser = testingUser,
                UpdateUserId = 1,
                UpdatedAt = DateTime.UtcNow,
                Email = testingUser.Email,
                Identifier = "testingIdentifierTwo",
                User = testingUser,
                UserId = testingUser.Id
            };
            context.Add(testingAuth0UserTwo);
            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = testingAuth0UserOne.Email,
                EmailVerified = true,
                Identifier = testingAuth0UserOne.Identifier,
                GivenName = testingAuth0UserOne.FirstName,
                FamilyName = testingAuth0UserOne.LastName,
                Locale = "pl",
                Name = testingAuth0UserOne.FirstName + testingAuth0UserOne.LastName,
                Nickname = testingAuth0UserOne.Nickname,
                UpdatedAt = DateTime.UtcNow,
            });
            //Act
            var executed = await action.Execute();

            //Assert

            Assert.IsNotNull(executed);
            var result = executed.Value as List<Auth0User>;
            Assert.AreEqual(2, result!.Count);
            Assert.AreEqual(2, context.Auth0Users.Count());
            Assert.AreEqual(result!.Count, context.Auth0Users.Count());
            Assert.IsTrue(result.Any(c => c.Identifier == "testingIdentifierOne"));
            Assert.IsTrue(result.Any(c => c.Identifier == "testingIdentifierTwo"));
        }

        [TestMethod()]
        public async Task GetAuth0UsersByCurrentUserActionTests_UserDataValidationThrowsError()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetAuth0UsersByCurrentUserAction>();

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
            var exception = await Assert.ThrowsExceptionAsync<HttpError500Exception>(() => action.Execute());

            //Assert
            Assert.IsInstanceOfType(exception, typeof(HttpError500Exception));
            Assert.AreEqual("Sorry we couldn't fetch your Auth0 data. Please, contact system administrator", exception.Message);
        }
    }
}
