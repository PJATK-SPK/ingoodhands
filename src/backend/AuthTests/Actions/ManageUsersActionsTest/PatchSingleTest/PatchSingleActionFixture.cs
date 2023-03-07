using Auth.Actions.ManageUsersActions.ManagerUsersPatchSingle;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;

namespace AuthTests.Actions.ManageUsersActionsTest.PatchSingleTest
{
    public static class PatchSingleActionFixture
    {
        public static User CreateUser(string firstName, string lastName, long? warehouseId = null) => new()
        {
            Status = DbEntityStatus.Active,
            FirstName = firstName,
            LastName = lastName,
            Email = firstName + "@" + lastName + ".com",
            WarehouseId = warehouseId
        };

        public static Auth0User CreateAuth0User(User user, int id) => new()
        {
            FirstName = "Auth",
            LastName = "Auth0User" + id,
            Nickname = "Auth0",
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Email = user.Email,
            Identifier = user.FirstName + user.LastName + "GoogleId",
            User = user,
            UserId = user.Id
        };

        public static UserRole CreateUserRole(User user, long roleId) => new()
        {
            RoleId = roleId,
            User = user,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active
        };

        public static ManageUsersPatchSinglePayload CreatePatchingPayload(RoleName roleName, string? warehouseId = null) => new()
        {
            WarehouseId = warehouseId,
            Roles = new List<string>
            {
                roleName.ToString()
            }
        };

        public static ManageUsersPatchSinglePayload CreatePatchingPayload(RoleName roleName1, RoleName roleName2, string? warehouseId = null) => new()
        {
            WarehouseId = warehouseId,
            Roles = new List<string>
            {
                roleName1.ToString(),
                roleName2.ToString()
            }
        };

        public static ManageUsersPatchSinglePayload CreatePatchingPayload(RoleName roleName1, RoleName roleName2, RoleName roleName3, string? warehouseId = null) => new()
        {
            WarehouseId = warehouseId,
            Roles = new List<string>
            {
                roleName1.ToString(),
                roleName2.ToString(),
                roleName3.ToString()
            }
        };
    }
}
