using Core.Database.Enums;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace Core.Database.Seeders
{
    public static class WarehouseSeeder
    {
        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 1,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                ShortName = "PL001",
                AddressId = AddressSeeder.Address1Poland.Id
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 2,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                ShortName = "PL002",
                AddressId = AddressSeeder.Address2Poland.Id
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 3,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                ShortName = "PL003",
                AddressId = AddressSeeder.Address3Poland.Id
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 4,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                ShortName = "DE001",
                AddressId = AddressSeeder.Address4Germany.Id
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 5,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                ShortName = "DE002",
                AddressId = AddressSeeder.Address5Germany.Id
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 6,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                ShortName = "HU001",
                AddressId = AddressSeeder.Address6Hungary.Id
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 7,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                ShortName = "CZ001",
                AddressId = AddressSeeder.Address7Czech.Id
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 8,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                ShortName = "FR001",
                AddressId = AddressSeeder.Address8France.Id
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 9,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                ShortName = "FR002",
                AddressId = AddressSeeder.Address9France.Id
            });
        }
    }
}