using Auth;
using Auth.Actions.UserSettingsActions.GetUserDetails;
using Auth.Models;
using Autofac;
using Core;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Dynamic.Core;
using TestsBase;

namespace AuthTests.Actions.UserSettings
{
    [TestClass()]
    public class GetCurrentUserTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new AuthModule(),
        };

        [TestMethod()]
        public async Task GetCurrentUserTest_GetUserFromDatabase()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetUserDetailsAction>();

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
            //Act
            var executed = await action.Execute();

            //Assert
            var result = executed.Value as UserDetailsResponse;
            Assert.AreEqual(testingUser.Id, toolkit.Hashids.DecodeSingleLong(result!.Id));
            Assert.AreEqual("test@testing.com", result!.Email);
            Assert.AreEqual(testingUser.Auth0Users!.Count, result.Auth0Identifiers.Count);
            Assert.AreEqual(toolkit.Hashids.DecodeSingleLong(result.Id), context.Users.First(c => c.Id == toolkit.Hashids.DecodeSingleLong(result.Id)).Id);
        }

        [TestMethod()]
        public async Task GetCurrentUserTest_UserDataValidationThrowsError()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetUserDetailsAction>();

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
    }
}