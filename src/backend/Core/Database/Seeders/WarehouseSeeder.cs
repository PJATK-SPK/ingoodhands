using Core.Database.Enums;
using Core.Database.Models.Auth;
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
                ShortName = "PLN001",
                UpdatedAt = DateTime.UtcNow,
                UpdateUserId = 1,
                Address = new Address()
                {
                    Id = 1,
                    Status = DbEntityStatus.Active,
                    UpdatedAt = DateTime.UtcNow,
                    UpdateUserId = 1,
                }
           
            });
        }
    }
}
