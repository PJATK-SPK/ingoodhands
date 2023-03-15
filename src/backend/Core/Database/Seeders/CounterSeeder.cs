using Core.Database.Enums;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace Core.Database.Seeders
{
    public class CounterSeeder
    {
        public static readonly Counter DonationCounter = new()
        {
            Id = 1,
            Name = TableName.Donations,
            Value = 0,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            Status = DbEntityStatus.Active
        };

        public static readonly Counter OrderCounter = new()
        {
            Id = 2,
            Name = TableName.Orders,
            Value = 0,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            Status = DbEntityStatus.Active
        };

        public static readonly Counter DeliveryCounter = new()
        {
            Id = 3,
            Name = TableName.Deliveries,
            Value = 0,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            Status = DbEntityStatus.Active
        };

        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<Counter>().HasData(DonationCounter);
            builder.Entity<Counter>().HasData(OrderCounter);
            builder.Entity<Counter>().HasData(DeliveryCounter);
        }
    }
}
