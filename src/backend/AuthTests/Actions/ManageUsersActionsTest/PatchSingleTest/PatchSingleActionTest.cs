﻿using Auth;
using Auth.Actions.ManageUsersActions.ManagerUsersPatchSingle;
using Auth.Actions.ManageUsersActions.ManageUsersGetSingle;
using Auth.Actions.ManageUsersActions.ManageUsersPatchSingle;
using AuthTests.Actions.ManageUsersActionsTest.GetListTest;
using Autofac;
using Core;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace AuthTests.Actions.ManageUsersActionsTest.PatchSingleTest
{
    [TestClass()]
    public class PatchSingleActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new AuthModule(),
        };

        [TestMethod()]
        public async Task PatchSingleActionTest_ChangeRolesAndWarehouseId()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<ManageUsersPatchSingleAction>();

            // Arrange
            var roleDonorId = context.Roles.First(c => c.Name == RoleName.Donor).Id;
            var roleAdminId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;
            var roleDelivererId = context.Roles.First(c => c.Name == RoleName.Deliverer).Id;

            var testingUser1 = PatchSingleActionFixture.CreateUser("Normal", "User", 2);
            var testingAuth0User1 = PatchSingleActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role = PatchSingleActionFixture.CreateUserRole(testingUser1, roleDonorId);
            var testUser1Role2 = PatchSingleActionFixture.CreateUserRole(testingUser1, roleAdminId);
            var testUser1Role3 = PatchSingleActionFixture.CreateUserRole(testingUser1, roleDelivererId);

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
            var encodedWarehouseId = toolkit.Hashids.EncodeLong(1);
            var payLoad = PatchSingleActionFixture.CreatePatchingPayload(RoleName.Administrator, RoleName.Needy, encodedWarehouseId);
            var encodedUserId = toolkit.Hashids.EncodeLong(testingUser1.Id);

            // Act
            var executed = await action.Execute(encodedUserId, payLoad);
            var result = executed.Value as ManageUsersPatchSingleResponseItem;

            // Assert
            Assert.IsTrue(result!.Roles.Any());
            Assert.AreEqual(2, result!.Roles.Count);
            Assert.AreEqual(1, toolkit.Hashids.DecodeSingleLong(result.WarehouseId));
            Assert.AreEqual(testingUser1.FirstName + " " + testingUser1.LastName, result!.FullName);
        }

        [TestMethod()]
        public async Task PatchSingleActionTest_NoWarehouseIdInPayload_NullUserWarehouseId()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<ManageUsersPatchSingleAction>();

            // Arrange
            var roleDonorId = context.Roles.First(c => c.Name == RoleName.Donor).Id;
            var roleAdminId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;
            var roleDelivererId = context.Roles.First(c => c.Name == RoleName.Deliverer).Id;

            var testingUser1 = PatchSingleActionFixture.CreateUser("Normal", "User", 4);
            var testingAuth0User1 = PatchSingleActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = PatchSingleActionFixture.CreateUserRole(testingUser1, roleAdminId);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

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
            var payLoad = PatchSingleActionFixture.CreatePatchingPayload(RoleName.Administrator, RoleName.Needy, RoleName.Deliverer);
            var encodedUserId = toolkit.Hashids.EncodeLong(testingUser1.Id);

            // Act
            var executed = await action.Execute(encodedUserId, payLoad);
            var result = executed.Value as ManageUsersPatchSingleResponseItem;
            var userFromDb = context.Users.FirstOrDefault(u => u.Id == testingUser1.Id);

            // Assert
            Assert.IsTrue(result!.Roles.Any());
            Assert.AreEqual(3, result!.Roles.Count);
            Assert.IsTrue(result.WarehouseId == null);
            Assert.IsNull(userFromDb!.WarehouseId);
            Assert.AreEqual(testingUser1.FirstName + " " + testingUser1.LastName, result!.FullName);
        }

        [TestMethod()]
        public async Task PatchSingleActionTest_NoUserOfGivenIdInDb_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<ManageUsersPatchSingleAction>();

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
            var encodedWarehouseId = toolkit.Hashids.EncodeLong(1);
            var payLoad = PatchSingleActionFixture.CreatePatchingPayload(RoleName.Needy, encodedWarehouseId);
            var encodedUserIdThatIsNotInDatabase = toolkit.Hashids.EncodeLong(userIdThatIsNotInDatabase);

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(encodedUserIdThatIsNotInDatabase, payLoad));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }

        [TestMethod()]
        public async Task PatchSingleActionTest_NoRolesInpayload_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<ManageUsersPatchSingleAction>();

            // Arrange
            var roleAdminId = context.Roles.First(c => c.Name == RoleName.Administrator).Id;

            var testingUser1 = GetListActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = GetListActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = GetListActionFixture.CreateUserRole(testingUser1, roleAdminId);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

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

            var payLoad = new ManageUsersPatchSinglePayload()
            {
                WarehouseId = null,
                Roles = new List<string>()
            };

            var encodedUserId = toolkit.Hashids.EncodeLong(testingUser1.Id);

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ClientInputErrorException>(() => action.Execute(encodedUserId, payLoad));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ClientInputErrorException));
            Assert.IsNotNull(exception.Message);
        }
    }
}
