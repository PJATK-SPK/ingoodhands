using Core.Database.Enums;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace Core.Database.Seeders
{
    public static class ProductSeeder
    {
        public static readonly Product Product1Rice = new()
        {
            Id = 1,
            Name = "Rice",
            Unit = UnitType.Kg,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product2Pasta = new()
        {
            Id = 2,
            Name = "Pasta",
            Unit = UnitType.Kg,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product3Cereals = new()
        {
            Id = 3,
            Name = "Cereals",
            Unit = UnitType.Kg,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product4Groats = new()
        {
            Id = 4,
            Name = "Groats",
            Unit = UnitType.Kg,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product5Walnuts = new()
        {
            Id = 5,
            Name = "Walnuts",
            Unit = UnitType.Kg,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product6Delicacies = new()
        {
            Id = 6,
            Name = "Delicacies",
            Unit = UnitType.Kg,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product7Flour = new()
        {
            Id = 7,
            Name = "Flour",
            Unit = UnitType.Kg,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product8Water = new()
        {
            Id = 8,
            Name = "Water",
            Unit = UnitType.L,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product9Milk = new()
        {
            Id = 9,
            Name = "Milk",
            Unit = UnitType.L,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product10BubbleBath = new()
        {
            Id = 10,
            Name = "Bubble bath",
            Unit = UnitType.L,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product11Juice = new()
        {
            Id = 11,
            Name = "Juice",
            Unit = UnitType.L,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product12EnergyDrink = new()
        {
            Id = 12,
            Name = "Energy drink",
            Unit = UnitType.L,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product13Soup = new()
        {
            Id = 13,
            Name = "Soup",
            Unit = UnitType.L,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product14CannedFood = new()
        {
            Id = 14,
            Name = "Canned food",
            Unit = UnitType.Pcs,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product15Toothbrushes = new()
        {
            Id = 15,
            Name = "Toothbrushes",
            Unit = UnitType.Pcs,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product16Diapers = new()
        {
            Id = 16,
            Name = "Diapers",
            Unit = UnitType.Pcs,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product17Disinfectants = new()
        {
            Id = 17,
            Name = "Disinfectants",
            Unit = UnitType.Pcs,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product18ToiletPaper = new()
        {
            Id = 18,
            Name = "Toilet paper",
            Unit = UnitType.Pcs,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static readonly Product Product19Medicines = new()
        {
            Id = 19,
            Name = "Medicines",
            Unit = UnitType.Pcs,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            Status = DbEntityStatus.Active
        };

        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(Product1Rice);
            builder.Entity<Product>().HasData(Product2Pasta);
            builder.Entity<Product>().HasData(Product3Cereals);
            builder.Entity<Product>().HasData(Product4Groats);
            builder.Entity<Product>().HasData(Product5Walnuts);
            builder.Entity<Product>().HasData(Product6Delicacies);
            builder.Entity<Product>().HasData(Product7Flour);
            builder.Entity<Product>().HasData(Product8Water);
            builder.Entity<Product>().HasData(Product9Milk);
            builder.Entity<Product>().HasData(Product10BubbleBath);
            builder.Entity<Product>().HasData(Product11Juice);
            builder.Entity<Product>().HasData(Product12EnergyDrink);
            builder.Entity<Product>().HasData(Product13Soup);
            builder.Entity<Product>().HasData(Product14CannedFood);
            builder.Entity<Product>().HasData(Product15Toothbrushes);
            builder.Entity<Product>().HasData(Product16Diapers);
            builder.Entity<Product>().HasData(Product17Disinfectants);
            builder.Entity<Product>().HasData(Product18ToiletPaper);
            builder.Entity<Product>().HasData(Product19Medicines);
        }
    }
}
