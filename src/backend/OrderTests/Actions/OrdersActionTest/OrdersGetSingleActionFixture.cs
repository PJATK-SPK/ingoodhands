using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Setup.Auth0;

namespace OrderTests.Actions.OrdersActionTest
{
    public static class OrdersGetSingleActionFixture
    {
        public static User CreateUser(string firstName, string lastName) => new()
        {
            Status = DbEntityStatus.Active,
            FirstName = firstName,
            LastName = lastName,
            Email = firstName + "@" + lastName + ".com",
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

        public static Order CreateOrder(User user, Address address, string orderName = "ORD000001") => new()
        {
            Name = orderName,
            Percentage = 50,
            AddressId = address.Id,
            OwnerUser = user,
            CreationDate = DateTime.UtcNow,
            IsCanceledByUser = false,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active
        };

        public static OrderProduct CreateOrderProduct(Order order, long productId, int quantity) => new()
        {
            Order = order,
            ProductId = productId,
            Quantity = quantity,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active
        };

        public static Delivery CreateDelivery(User user, Address address, Order order, bool isDelivered, Warehouse warehouse, string deliveryName = "DEL000001") => new()
        {
            Name = deliveryName,
            Order = order,
            IsDelivered = isDelivered,
            DelivererUser = user,
            CreationDate = DateTime.UtcNow,
            IsLost = false,
            IsPacked = true,
            WarehouseId = warehouse.Id,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active
        };

        public static DeliveryProduct CreateDeliveryProduct(Delivery delivery, long productId, int quantity) => new()
        {
            Delivery = delivery,
            ProductId = productId,
            Quantity = quantity,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active,
        };
    }
}
