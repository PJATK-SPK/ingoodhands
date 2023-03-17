using Auth;
using Auth.Actions.ManageUsersActions.ManageUsersGetList;
using Autofac;
using Core;
using Core.Database;
using Core.Database.Enums;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Dynamic.Core;
using TestsBase;

namespace AuthTests.Actions.ManageUsers
{
    [TestClass()]
    public class GetListTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new AuthModule(),
        };

        [TestMethod()]
        public async Task GetListTest_ReturnTwoUsersWithRoles()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<ManageUsersGetListAction>();

            // Arrange
            var roleDonorId = context.Roles.First(c => c.Name == RoleName.Donor).Id;
            var roleAdminId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;
            var roleDelivererId = context.Roles.First(c => c.Name == RoleName.Deliverer).Id;

            var testingUser1 = GetListFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = GetListFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role = GetListFixture.CreateUserRole(testingUser1, roleDonorId);
            var testUser1Role2 = GetListFixture.CreateUserRole(testingUser1, roleAdminId);
            var testUser1Role3 = GetListFixture.CreateUserRole(testingUser1, roleDelivererId);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role);
            context.Add(testUser1Role2);
            context.Add(testUser1Role3);

            // Second User
            var testingUser2 = GetListFixture.CreateUser("Normal", "User2");
            var testingAuth0User2 = GetListFixture.CreateAuth0User(testingUser2, 2);
            var testUser2Role = GetListFixture.CreateUserRole(testingUser2, roleDonorId);

            context.Add(testingUser2);
            context.Add(testingAuth0User2);
            context.Add(testUser2Role);

            var testingUser3 = GetListFixture.CreateUser("Normal", "User3");
            var testingAuth0User3 = GetListFixture.CreateAuth0User(testingUser3, 3);
            var testUser3Role = GetListFixture.CreateUserRole(testingUser3, roleDonorId);

            context.Add(testingUser3);
            context.Add(testingAuth0User3);
            context.Add(testUser3Role);
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

            // Act
            var executed = await action.Execute(1, 100);
            var result = executed.Value as PagedResult<ManageUsersGetListResponseItem>;

            // Assert
            Assert.IsTrue(result!.Queryable.Any());
            Assert.AreEqual(3, result!.Queryable.Count());
        }

        [TestMethod()]
        public async Task GetListTest_ReturnFilteredUser()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<ManageUsersGetListAction>();

            // Arrange
            var roleDonorId = context.Roles.First(c => c.Name == RoleName.Donor).Id;
            var roleAdminId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;
            var roleDelivererId = context.Roles.First(c => c.Name == RoleName.Deliverer).Id;

            var testingUser1 = GetListFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = GetListFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role = GetListFixture.CreateUserRole(testingUser1, roleDonorId);
            var testUser1Role2 = GetListFixture.CreateUserRole(testingUser1, roleAdminId);
            var testUser1Role3 = GetListFixture.CreateUserRole(testingUser1, roleDelivererId);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role);
            context.Add(testUser1Role2);
            context.Add(testUser1Role3);

            // Second User
            var testingUser2 = GetListFixture.CreateUser("Herman", "Dziad");
            var testingAuth0User2 = GetListFixture.CreateAuth0User(testingUser2, 2);
            var testUser2Role = GetListFixture.CreateUserRole(testingUser2, roleDonorId);

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

            // Act
            var executed = await action.Execute(1, 100, "Normal User");
            var result = executed.Value as PagedResult<ManageUsersGetListResponseItem>;

            // Assert
            Assert.IsTrue(result!.Queryable.Any());
            Assert.AreEqual(1, result!.Queryable.Count());
        }
    }
}
