using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Database.Seeders;

namespace DonateTests.Jobs.SetExpiredDonations
{
    public class SetExpiredDonationsJobFixture
    {
        public static Donation CreateDonation(string name = "DNT000001") => new()
        {
            CreationDate = DateTime.UtcNow.AddDays(-5),
            CreationUserId = UserSeeder.ServierUser.Id,
            IsDelivered = false,
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
