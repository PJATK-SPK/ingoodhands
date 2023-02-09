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
                UpdatedAt = new DateTime(2023, 01, 01),
                UpdateUserId = 1,
                ShortName = "PL001",
                Address = new Address()
                {
                    Id = 1,
                    Status = DbEntityStatus.Active,
                    UpdatedAt = new DateTime(2023, 01, 01),
                    UpdateUserId = 1,
                    CountryId = 177,
                    PostalCode = "80-503",
                    City = "Gdansk",
                    Street = "al. gen. J. Hallera",
                    StreetNumber = "239",
                    GpsLatitude = 54.40672743695016, 
                    GpsLongitude = 18.626590482862696
                }
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 2,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01),
                UpdateUserId = 1,
                ShortName = "PL002",
                Address = new Address()
                {
                    Id = 2,
                    Status = DbEntityStatus.Active,
                    UpdatedAt = new DateTime(2023, 01, 01),
                    UpdateUserId = 1,
                    CountryId = 177, 
                    PostalCode = "30-701",
                    City = "Krakow",
                    Street = "ul. Zablocie",
                    StreetNumber = "20",
                    Apartment = "22",
                    GpsLatitude = 50.050510416252955,
                    GpsLongitude = 19.96738355797516
                }
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 3,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01),
                UpdateUserId = 1,
                ShortName = "PL003",
                Address = new Address()
                {
                    Id = 3,
                    Status = DbEntityStatus.Active,
                    UpdatedAt = new DateTime(2023, 01, 01),
                    UpdateUserId = 1,
                    CountryId = 177,
                    PostalCode = "05-820",
                    City = "Piastow",
                    Street = "ul. St. Bodycha",
                    StreetNumber = "97",
                    GpsLatitude = 52.180876508945325,
                    GpsLongitude = 20.864602669100716
                }
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 4,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01),
                UpdateUserId = 1,
                ShortName = "DE001",
                Address = new Address()
                {
                    Id = 4,
                    Status = DbEntityStatus.Active,
                    UpdatedAt = new DateTime(2023, 01, 01),
                    UpdateUserId = 1,
                    CountryId = 84,
                    PostalCode = "13435",
                    City = "Berlin",
                    Street = "Wallenroder Strasse",
                    StreetNumber = "7",
                    GpsLatitude = 52.6243741245117,  
                    GpsLongitude = 13.337297904503853
                }
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 5,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01),
                UpdateUserId = 1,
                ShortName = "DE002",
                Address = new Address()
                {
                    Id = 5,
                    Status = DbEntityStatus.Active,
                    UpdatedAt = new DateTime(2023, 01, 01),
                    UpdateUserId = 1,
                    CountryId = 84,
                    PostalCode = "81677",
                    City = "Munich",
                    Street = "Kronstadter Str", 
                    StreetNumber = "30",
                    GpsLatitude = 48.141578533011334,  
                    GpsLongitude = 11.648666754927092
                }
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 6,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01),
                UpdateUserId = 1,
                ShortName = "HU001",
                Address = new Address()
                {
                    Id = 6,
                    Status = DbEntityStatus.Active,
                    UpdatedAt = new DateTime(2023, 01, 01),
                    UpdateUserId = 1,
                    CountryId = 101,
                    PostalCode = "1097",
                    City = "Budapest", 
                    Street = "Feherakac u.",
                    StreetNumber = "821",
                    GpsLatitude = 47.469941092986005,
                    GpsLongitude = 19.109420384400256
                }
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 7,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01),
                UpdateUserId = 1,
                ShortName = "CZ001",
                Address = new Address()
                {
                    Id = 7,
                    Status = DbEntityStatus.Active,
                    UpdatedAt = new DateTime(2023, 01, 01),
                    UpdateUserId = 1,
                    CountryId = 61,
                    PostalCode = "17000",
                    City = "Prague",
                    Street = "Argentinska",
                    StreetNumber = "516",
                    Apartment = "40",
                    GpsLatitude = 50.12465893170012,
                    GpsLongitude = 14.439680032281526
                }
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 8,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01),
                UpdateUserId = 1,
                ShortName = "FR001",
                Address = new Address()
                {
                    Id = 8,
                    Status = DbEntityStatus.Active,
                    UpdatedAt = new DateTime(2023, 01, 01),
                    UpdateUserId = 1,
                    CountryId = 77,
                    PostalCode = "06200",
                    City = "Nice",
                    Street = "Pass. du Fret",
                    GpsLatitude = 43.66614093758796,
                    GpsLongitude = 7.202632419942202
                }
            });
            builder.Entity<Warehouse>().HasData(new Warehouse()
            {
                Id = 9,
                Status = DbEntityStatus.Active,
                UpdatedAt = new DateTime(2023, 01, 01),
                UpdateUserId = 1,
                ShortName = "FR002",
                Address = new Address()
                {
                    Id = 9,
                    Status = DbEntityStatus.Active,
                    UpdatedAt = new DateTime(2023, 01, 01),
                    UpdateUserId = 1,
                    CountryId = 77,
                    PostalCode = "13435",
                    City = "Berlin",
                    Street = "Wallenroder Strasse",
                    StreetNumber = "7",
                    GpsLatitude = 52.6243741245117,  
                    GpsLongitude = 13.337297904503853
                }
            });
        }
    }
}