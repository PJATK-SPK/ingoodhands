using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Donate.Shared;

namespace DonateTests.Jobs.SetExpiredDonations
{
    public static class SetExpiredDonationsJobFixture
    {
        public static Donation CreateDonation(string name = "DNT000001") => new()
        {
            CreationDate = DateTime.UtcNow.AddDays(-5),
            CreationUserId = UserSeeder.ServierUser.Id,
            IsDelivered = false,
            ExpirationDate = ExpireDateService.GetExpiredDate4Donation(DateTime.UtcNow.AddDays(-5)),
            IsExpired = false,
            IsIncludedInStock = false,
            Name = name,
            Status = DbEntityStatus.Active,
            UpdatedAt = DateTime.UtcNow,
            UpdateUserId = UserSeeder.ServierUser.Id,
            WarehouseId = WarehouseSeeder.Warehouse1PL.Id,
        };
    }
}
