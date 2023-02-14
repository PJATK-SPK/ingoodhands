using Core.Database.Enums;
using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace Core.Database.Seeders
{
    public static class RoleSeeder
    {

        public static readonly Role Role1Administrator = new()
        {
            Id = 1,
            Name = RoleName.Administrator,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServierUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Role Role2Donor = new()
        {
            Id = 2,
            Name = RoleName.Donor,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServierUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Role Role3Needy = new()
        {
            Id = 3,
            Name = RoleName.Needy,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServierUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Role Role4WarehouseKeeper = new()
        {
            Id = 4,
            Name = RoleName.WarehouseKeeper,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServierUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Role Role5Deliverer = new()
        {
            Id = 5,
            Name = RoleName.Deliverer,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServierUser.Id,
            Status = DbEntityStatus.Active
        };

        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(Role1Administrator);
            builder.Entity<Role>().HasData(Role2Donor);
            builder.Entity<Role>().HasData(Role3Needy);
            builder.Entity<Role>().HasData(Role4WarehouseKeeper);
            builder.Entity<Role>().HasData(Role5Deliverer);
        }
    }
}
