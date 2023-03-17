using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Setup.Auth0;

namespace OrderTests.Actions.Stock
{
    public static class StocksGetListFixture
    {
        public static User CreateUser(string firstName, string lastName, long warehouseId) => new()
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

        public static CurrentUserInfo GetCurrentUserInfo(Auth0User auth0User) => new()
        {
            Email = auth0User.Email,
            EmailVerified = true,
            Identifier = auth0User.Identifier,
            GivenName = auth0User.FirstName,
            FamilyName = auth0User.LastName,
            Locale = "pl",
            Name = auth0User.FirstName + auth0User.LastName,
            Nickname = auth0User.Nickname,
            UpdatedAt = DateTime.UtcNow,
        };

        public static UserRole CreateUserRole(User user, long roleId) => new()
        {
            RoleId = roleId,
            User = user,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active
        };

        public static Core.Database.Models.Core.Stock CreateStock(Product product, int quantity, long warehouseId) => new()
        {
            ProductId = product.Id,
            Quantity = quantity,
            WarehouseId = warehouseId,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active
        };
    }
}
