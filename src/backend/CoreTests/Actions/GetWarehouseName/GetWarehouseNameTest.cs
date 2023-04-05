using Core.Actions.DonateForm.GetProducts;
using Core.Database;
using Core.Setup.Enums;
using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsBase;
using Autofac;
using Core.Database.Seeders;
using Core.Actions.WarehouseName.GetWarehouseName;
using Core.Exceptions;

namespace CoreTests.Actions.GetWarehouseName
{
    [TestClass()]
    public class GetWarehouseNameTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
        };

        [TestMethod()]
        public async Task GetWarehouseName_ReturnsUsersWarehouseName()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetWarehouseNameAction>();

            // Arrange
            var testingUser1 = GetWarehouseNameFixture.CreateUser("Normal", "User", WarehouseSeeder.Warehouse1PL.Id);
            var testingAuth0User1 = GetWarehouseNameFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = GetWarehouseNameFixture.CreateUserRole(testingUser1, RoleSeeder.Role4WarehouseKeeper.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(GetWarehouseNameFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute();
            var result = executed.Value as GetWarehouseNameResponse;

            // Assert
            Assert.AreEqual("PL001", result!.WarehouseName);
        }

        [TestMethod()]
        public async Task UserWithNoWarehouse_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetWarehouseNameAction>();

            // Arrange
            var testingUser1 = GetWarehouseNameFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = GetWarehouseNameFixture.CreateAuth0User(testingUser1, 1);
            var testUserRole1 = GetWarehouseNameFixture.CreateUserRole(testingUser1, RoleSeeder.Role4WarehouseKeeper.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUserRole1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(GetWarehouseNameFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute());

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}
