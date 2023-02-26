using Core.Database.Models.Core;

namespace Donate.Shared
{
    public static class ExpireDateService
    {
        public static readonly int ExpireDays = 30;

        public static DateTime GetExpiredDate4Donation(Donation donation) => GetExpiredDate4Donation(donation.CreationDate);
        public static DateTime GetExpiredDate4Donation(DateTime donationCreationDate) => donationCreationDate.Date.AddDays(ExpireDays);
    }
}
