﻿using AuthService;
using AuthService.Actions.UserSettingsActions.PatchUserDetails;
using AuthService.Models;
using Autofac;
using Core;
using Core.Auth0;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Dynamic.Core;
using TestsBase;

namespace AuthServiceTests.Actions.UserSettingsTests.PatchUserDetails
{
    [TestClass()]
    public class PatchUserDetailsActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(false),
            new AuthServiceModule(),
        };

        [TestMethod()]
        public async Task PatchUserDetailsActionTest_PatchFirstNameAndLastName()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PatchUserDetailsAction>();

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

            var testPayload = new PatchUserDetailsPayload
            {
                FirstName = "Test",
                LastName = "Works"
            };
            //Act
            var executed = await action.Execute(testPayload, toolkit.Hashids.EncodeLong(testingUser.Id));

            //Assert
            var result = executed.Value as UserDetailsResponse;

            Assert.IsNotNull(executed);
            //Count should be 2, because there's also serviceUser in User database         
            Assert.AreEqual(2, context.Users.Count());
            Assert.AreEqual("Test", result!.FirstName);
            Assert.AreEqual("Works", result!.LastName);
            Assert.IsTrue(context.Users.Any(c => c.FirstName == "Test"));
            Assert.IsTrue(context.Users.Any(c => c.LastName == "Works"));
        }

        [TestMethod()]
        public async Task PatchUserDetailsActionTest_ThrowExceptionOnPayloadNull()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PatchUserDetailsAction>();

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

            var testPayload = new PatchUserDetailsPayload { };

            //Act
            var exception = await Assert.ThrowsExceptionAsync<HttpError400Exception>(() => action.Execute(testPayload, toolkit.Hashids.EncodeLong(testingUser.Id)));

            //Assert
            Assert.IsInstanceOfType(exception, typeof(HttpError400Exception));
        }

        [TestMethod()]
        public async Task PatchUserDetailsActionTest_ThrowExceptionOnPayloadExceedingDbFieldLength()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PatchUserDetailsAction>();

            // Arrange          
            var testPayload = new PatchUserDetailsPayload
            {
                FirstName = "AbcdefghijklmnouprstxyzAbcdefghijklmnouprstxyzAbcdefghijklmnouprstxyz", //69 letters to varchar(50)
                LastName = "Works"
            };

            //Act
            var exception = await Assert.ThrowsExceptionAsync<HttpError400Exception>(() => action.Execute(testPayload, toolkit.Hashids.EncodeLong(5)));

            //Assert
            Assert.IsInstanceOfType(exception, typeof(HttpError400Exception));
        }

        [TestMethod()]
        public async Task PatchUserDetailsActionTest_ThrowExceptionOnNoUsersInDatabase()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<PatchUserDetailsAction>();

            // Arrange          
            var testPayload = new PatchUserDetailsPayload
            {
                FirstName = "Test",
                LastName = "Works"
            };

            //Act
            var exception = await Assert.ThrowsExceptionAsync<HttpError500Exception>(() => action.Execute(testPayload, toolkit.Hashids.EncodeLong(5)));

            //Assert
            Assert.IsInstanceOfType(exception, typeof(HttpError500Exception));
            Assert.AreEqual("Sorry we couldn't find your user in database. Please, contact system administrator", exception.Message);
        }
    }
}