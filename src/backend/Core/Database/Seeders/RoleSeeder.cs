using Core.Database.Enums;
using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Seeders
{
    internal class RoleSeeder
    {
        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Name = RoleName.Administrator,
                UpdatedAt = DateTime.UtcNow,
                UpdateUserId = 1,
                Status = Core.Database.Enums.DbEntityStatus.Active
            });

            builder.Entity<Role>().HasData(new Role
            {
                Id = 2,
                Name = RoleName.Donor,
                UpdatedAt = DateTime.UtcNow,
                UpdateUserId = 1,
                Status = Core.Database.Enums.DbEntityStatus.Active
            });

            builder.Entity<Role>().HasData(new Role
            {
                Id = 3,
                Name = RoleName.Needy,
                UpdatedAt = DateTime.UtcNow,
                UpdateUserId = 1,
                Status = Core.Database.Enums.DbEntityStatus.Active
            });

            builder.Entity<Role>().HasData(new Role
            {
                Id = 4,
                Name = RoleName.WarehouseKeeper,
                UpdatedAt = DateTime.UtcNow,
                UpdateUserId = 1,
                Status = Core.Database.Enums.DbEntityStatus.Active
            });

            builder.Entity<Role>().HasData(new Role
            {
                Id = 5,
                Name = RoleName.Deliverer,
                UpdatedAt = DateTime.UtcNow,
                UpdateUserId = 1,
                Status = Core.Database.Enums.DbEntityStatus.Active
            });
        }
    }
}
