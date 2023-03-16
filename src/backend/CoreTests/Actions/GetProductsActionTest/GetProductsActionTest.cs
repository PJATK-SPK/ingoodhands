using Autofac;
using Core;
using Core.Actions.DonateForm.GetProducts;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace CoreTests.Actions.GetProductsActionTest
{
    [TestClass()]
    public class GetProductsActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
        };

        [TestMethod()]
        public async Task GetProductsActionTest_GeProducts_ReturnsProducts()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetProductsAction>();

            // Act
            var executed = await action.Execute();
            var result = executed.Value as List<GetProductsResponse>;

            // Assert
            Assert.IsTrue(result!.Any());
        }

        [TestMethod()]
        public async Task GetProductsActionTest_GeProductsWithRoleNeedy_ReturnsProducts()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetProductsAction>();
            // Arrange
            var testingUser1 = GetProductsActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = GetProductsActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role = GetProductsActionFixture.CreateUserRole(testingUser1, RoleSeeder.Role3Needy.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(GetProductsActionFixture.GetCurrentUserInfo(testingAuth0User1));
            // Act
            var executed = await action.Execute(RoleName.Needy);
            var result = executed.Value as List<GetProductsResponse>;

            // Assert
            Assert.IsTrue(result!.Any());
        }
    }
}
