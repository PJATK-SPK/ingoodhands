using Core.Database.Models.Core;

namespace Donate.Shared
{
    public class ExpireDateService
    {
        public readonly int ExpireDays = 30;

        public static DateTime GetExpiredDate4Today() => DateTime.UtcNow.Date.AddDays(-30);
        public static DateTime GetExpiredDate4Donation(Donation donation) => GetExpiredDate4Donation(donation.CreationDate);
        public static DateTime GetExpiredDate4Donation(DateTime donationCreationDate) => donationCreationDate.Date.AddDays(30);
    }
}
