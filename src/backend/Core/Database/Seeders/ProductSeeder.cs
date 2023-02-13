using Core.Database.Enums;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace Core.Database.Seeders
{
    public static class ProductSeeder
    {
        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 1,
                Name = "Rice",
                Unit = UnitType.Kg,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 2,
                Name = "Pasta",
                Unit = UnitType.Kg,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 3,
                Name = "Cereals",
                Unit = UnitType.Kg,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 3,
                Name = "Groats",
                Unit = UnitType.Kg,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 4,
                Name = "Walnuts",
                Unit = UnitType.Kg,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 5,
                Name = "Delicacies",
                Unit = UnitType.Kg,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 6,
                Name = "Flour",
                Unit = UnitType.Kg,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 7,
                Name = "Water",
                Unit = UnitType.L,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 8,
                Name = "Milk",
                Unit = UnitType.L,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 9,
                Name = "Bubble bath",
                Unit = UnitType.L,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 10,
                Name = "Juice",
                Unit = UnitType.L,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 11,
                Name = "Energy drink",
                Unit = UnitType.L,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 12,
                Name = "Soup",
                Unit = UnitType.L,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 13,
                Name = "Canned food",
                Unit = UnitType.Pcs,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 14,
                Name = "Toothbrushes",
                Unit = UnitType.Pcs,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 15,
                Name = "Diapers",
                Unit = UnitType.Pcs,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 16,
                Name = "Disinfectants",
                Unit = UnitType.Pcs,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 17,
                Name = "Toilet paper",
                Unit = UnitType.Pcs,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
            builder.Entity<Product>().HasData(new Product()
            {
                Id = 18,
                Name = "Medicines",
                Unit = UnitType.Pcs,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UpdateUserId = 1,
                Status = DbEntityStatus.Active
            });
        }
    }
}
