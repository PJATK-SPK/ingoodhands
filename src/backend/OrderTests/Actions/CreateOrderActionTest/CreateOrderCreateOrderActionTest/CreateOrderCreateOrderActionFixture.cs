using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;

namespace OrderTests.Actions.CreateOrderActionTest.CreateOrderCreateOrderActionTest
{
    public static class CreateOrderCreateOrderActionFixture
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
            FirstName = user.FirstName,
            LastName = user.LastName + id,
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
            UserId = user.Id,
            User = user,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active
        };

        public static OrderProduct CreateOrderProduct(Order product) => new()
        {

        };
    }
}
