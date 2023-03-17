using Auth;
using Auth.Actions.UserSettingsActions.PatchUserDetails;
using Auth.Models;
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

namespace AuthTests.Actions.UserSettings
{
    [TestClass()]
    public class PatchUserDetailsTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new AuthModule(),
        };

        [TestMethod()]
        public async Task PatchUserDetailsTest_PatchFirstNameAndLastName()
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
                Email = "test@testing.com",
                WarehouseId = null
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
        public async Task PatchUserDetailsTest_ThrowExceptionOnPayloadNull()
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
                Email = "test@testing.com",
                WarehouseId = null
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
            var exception = await Assert.ThrowsExceptionAsync<ValidationException>(() => action.Execute(testPayload, toolkit.Hashids.EncodeLong(testingUser.Id)));

            //Assert
            Assert.IsInstanceOfType(exception, typeof(ValidationException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task PatchUserDetailsTest_ThrowExceptionOnPayloadExceedingDbFieldLength()
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
            var exception = await Assert.ThrowsExceptionAsync<ValidationException>(() => action.Execute(testPayload, toolkit.Hashids.EncodeLong(5)));

            //Assert
            Assert.IsInstanceOfType(exception, typeof(ValidationException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task PatchUserDetailsTest_ThrowExceptionOnNoUsersInDatabase()
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
            var exception = await Assert.ThrowsExceptionAsync<ApplicationErrorException>(() => action.Execute(testPayload, toolkit.Hashids.EncodeLong(5)));

            //Assert
            Assert.IsInstanceOfType(exception, typeof(ApplicationErrorException));
            Assert.AreEqual("Sorry we couldn't find your user in database. Please, contact system administrator", exception.Message);
        }
    }
}
