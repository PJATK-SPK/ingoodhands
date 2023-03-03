using Auth;
using Auth.Actions.ManageUsersActions.ManageUsersGetSingle;
using AuthTests.Actions.ManageUsersActionsTest.GetListTest;
using Autofac;
using Core;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Exceptions;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Dynamic.Core;
using TestsBase;

namespace AuthTests.Actions.ManageUsersActionsTest.GetSingleTest
{
    [TestClass()]
    public class GetSingleActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new AuthModule(),
        };

        [TestMethod()]
        public async Task GetSingleActionTest_ReturnOneUserWithRoles()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<ManageUsersGetSingleAction>();

            // Arrange
            var roleDonorId = context.Roles.First(c => c.Name == RoleName.Donor).Id;
            var roleAdminId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;
            var roleDelivererId = context.Roles.First(c => c.Name == RoleName.Deliverer).Id;

            var testingUser1 = GetListActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = GetListActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role = GetListActionFixture.CreateUserRole(testingUser1, roleDonorId);
            var testUser1Role2 = GetListActionFixture.CreateUserRole(testingUser1, roleAdminId);
            var testUser1Role3 = GetListActionFixture.CreateUserRole(testingUser1, roleDelivererId);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role);
            context.Add(testUser1Role2);
            context.Add(testUser1Role3);

            // Second User
            var testingUser2 = GetListActionFixture.CreateUser("Normal", "User2");
            var testingAuth0User2 = GetListActionFixture.CreateAuth0User(testingUser2, 2);
            var testUser2Role = GetListActionFixture.CreateUserRole(testingUser2, roleDonorId);

            context.Add(testingUser2);
            context.Add(testingAuth0User2);
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

            var encodedUserId = toolkit.Hashids.EncodeLong(testingUser1.Id);

            // Act
            var executed = await action.Execute(encodedUserId);
            var result = executed.Value as GetSingleResponseItem;

            // Assert
            Assert.IsTrue(result!.Roles.Any());
            Assert.AreEqual(3, result!.Roles.Count);
            Assert.AreEqual(testingUser1.FirstName + " " + testingUser1.LastName, result!.FullName);
        }

        [TestMethod()]
        public async Task GetSingleActionTest_NoUserOfGivenIdInDb_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<ManageUsersGetSingleAction>();

            // Arrange
            var roleDonorId = context.Roles.First(c => c.Name == RoleName.Donor).Id;
            var roleAdminId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;
            var roleDelivererId = context.Roles.First(c => c.Name == RoleName.Deliverer).Id;
            var userIdThatIsNotInDatabase = 100;

            var testingUser1 = GetListActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = GetListActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role = GetListActionFixture.CreateUserRole(testingUser1, roleDonorId);
            var testUser1Role2 = GetListActionFixture.CreateUserRole(testingUser1, roleAdminId);
            var testUser1Role3 = GetListActionFixture.CreateUserRole(testingUser1, roleDelivererId);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role);
            context.Add(testUser1Role2);
            context.Add(testUser1Role3);

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
            var encodedUserIdThatIsNotInDatabase = toolkit.Hashids.EncodeLong(userIdThatIsNotInDatabase);

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(encodedUserIdThatIsNotInDatabase));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}
