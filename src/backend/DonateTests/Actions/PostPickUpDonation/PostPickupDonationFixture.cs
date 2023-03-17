using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Donate.Shared;

namespace DonateTests.Actions.PostPickUpDonation
{
    public static class PostPickupDonationFixture
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

        public static UserWebPush CreateUserWebPush(User user, string endpoint, string p256dh, string auth) => new()
        {
            User = user,
            Endpoint = endpoint,
            P256dh = p256dh,
            Auth = auth,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active,
        };

        public static Donation CreateDonation(bool isDelivered, string name = "DNT000001") => new()
        {
            CreationDate = DateTime.UtcNow.AddDays(-5),
            CreationUserId = UserSeeder.ServiceUser.Id,
            IsDelivered = isDelivered,
            ExpirationDate = ExpireDateService.GetExpiredDate4Donation(DateTime.UtcNow.AddDays(-5)),
            IsExpired = false,
            IsIncludedInStock = false,
            Name = name,
            Status = DbEntityStatus.Active,
            UpdatedAt = DateTime.UtcNow,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            WarehouseId = WarehouseSeeder.Warehouse1PL.Id,
        };
    }
}
