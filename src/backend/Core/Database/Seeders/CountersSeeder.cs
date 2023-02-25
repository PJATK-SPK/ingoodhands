using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Seeders
{
    public class CountersSeeder
    {
        public static readonly Counters DonationCounter = new()
        {
            Id = 1,
            Name = "Donations",
            Value = 0,
            UpdateUserId = UserSeeder.ServierUser.Id,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            Status = DbEntityStatus.Active
        };

        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<Counters>().HasData(DonationCounter);
        }
    }
}
