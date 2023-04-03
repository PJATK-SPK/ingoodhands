using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace Core.Database.Seeders
{
    public static class WarehouseSeeder
    {
        public static readonly Warehouse Warehouse1PL = new()
        {
            Id = 1,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            ShortName = "PL001",
            AddressId = AddressSeeder.Address1Poland.Id
        };

        public static readonly Warehouse Warehouse2PL = new()
        {
            Id = 2,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            ShortName = "PL002",
            AddressId = AddressSeeder.Address2Poland.Id
        };

        public static readonly Warehouse Warehouse3PL = new()
        {
            Id = 3,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            ShortName = "PL003",
            AddressId = AddressSeeder.Address3Poland.Id
        };
        
        public static readonly Warehouse Warehouse4DE = new()
        {
            Id = 5,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            ShortName = "DE001",
            AddressId = AddressSeeder.Address4Germany.Id
        };
        public static readonly Warehouse Warehouse5DE = new()
        {
            Id = 9,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            ShortName = "DE002",
            AddressId = AddressSeeder.Address5Germany.Id
        };
        public static readonly Warehouse Warehouse6HU = new()
        {
            Id = 6,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            ShortName = "HU001",
            AddressId = AddressSeeder.Address6Hungary.Id
        };

        public static readonly Warehouse Warehouse7CZ = new()
        {
            Id = 7,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            ShortName = "CZ001",
            AddressId = AddressSeeder.Address7Czech.Id
        };

        public static readonly Warehouse Warehouse8FR = new()
        {
            Id = 8,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            ShortName = "FR001",
            AddressId = AddressSeeder.Address8France.Id
        };
        
        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<Warehouse>().HasData(Warehouse1PL);
            builder.Entity<Warehouse>().HasData(Warehouse2PL);
            builder.Entity<Warehouse>().HasData(Warehouse3PL);
            builder.Entity<Warehouse>().HasData(Warehouse4DE);
            builder.Entity<Warehouse>().HasData(Warehouse5DE);
            builder.Entity<Warehouse>().HasData(Warehouse6HU);
            builder.Entity<Warehouse>().HasData(Warehouse7CZ);
            builder.Entity<Warehouse>().HasData(Warehouse8FR);
        }
    }
}