using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Donate.Shared;

namespace DonateTests.Jobs.IncludeToStock
{
    public static class IncludeToStockJobFixture
    {
        public static Donation CreateDonation(bool isDelivered, bool isIncludedInStock, string name = "DNT000001") => new()
        {
            CreationDate = DateTime.UtcNow.AddDays(-5),
            CreationUserId = UserSeeder.ServiceUser.Id,
            IsDelivered = isDelivered,
            ExpirationDate = ExpireDateService.GetExpiredDate4Donation(DateTime.UtcNow.AddDays(-5)),
            IsExpired = false,
            IsIncludedInStock = isIncludedInStock,
            Name = name,
            Status = DbEntityStatus.Active,
            UpdatedAt = DateTime.UtcNow,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            WarehouseId = WarehouseSeeder.Warehouse1PL.Id,
        };

        public static DonationProduct CreateDonationProduct(long donationId, long productId, int quantity) => new()
        {
            DonationId = donationId,
            ProductId = productId,
            Quantity = quantity,
            Status = DbEntityStatus.Active,
            UpdatedAt = DateTime.UtcNow,
            UpdateUserId = UserSeeder.ServiceUser.Id
        };
    }
}