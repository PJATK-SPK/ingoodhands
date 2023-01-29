﻿using AuthService;
using AuthService.BusinessLogic.Exceptions;
using AuthService.BusinessLogic.PostLogin;
using AuthService.BusinessLogic.UserSettings;
using Autofac;
using Core;
using Core.Auth0;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Dynamic.Core;
using TestsCore;
using static System.Net.Mime.MediaTypeNames;

namespace AuthServiceTests.BusinessLogic.UserSettings
{
    [TestClass()]
    public class UserSettingsActionTests
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(false),
            new AuthServiceModule(),
        };

        [TestMethod()]
        public async Task UserSettingsActionTest_GetAllAutho0UsersFromUser()
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
            Assert.AreEqual(testingUser.Auth0Users?[0].UserId, testingAuth0UserOne.UserId);
            Assert.AreEqual(testingUser.Auth0Users?[1].UserId, testingAuth0UserTwo.UserId);
        }

        [TestMethod()]
        public async Task UserSettingsActionTest_UserDataValidationThrowsError()
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
            var exception = await Assert.ThrowsExceptionAsync<InvalidAuth0DataException>(() => action.Execute());

            //Assert
            Assert.IsInstanceOfType(exception, typeof(InvalidAuth0DataException));
            Assert.AreEqual("Your Auth0User data is invalid, please contact system administrator", exception.Message);
        }
    }
}
