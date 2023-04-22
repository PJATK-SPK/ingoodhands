using Core.Database.Enums;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace Core.Database.Seeders
{
    public static class AddressSeeder
    {
        public static readonly Address Address1Poland = new()
        {
            Id = 1,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            CountryId = CountrySeeder.GetCountryId("PL"),
            PostalCode = "80-503",
            City = "Gdansk",
            Street = "al. gen. J. Hallera",
            StreetNumber = "239",
            GpsLatitude = 54.40672743695016,
            GpsLongitude = 18.626590482862696
        };

        public static readonly Address Address2Poland = new Address()
        {
            Id = 2,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            CountryId = CountrySeeder.GetCountryId("PL"),
            PostalCode = "30-701",
            City = "Krakow",
            Street = "ul. Zablocie",
            StreetNumber = "20",
            Apartment = "22",
            GpsLatitude = 50.050510416252955,
            GpsLongitude = 19.96738355797516
        };

        public static readonly Address Address3Poland = new()
        {
            Id = 3,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            CountryId = CountrySeeder.GetCountryId("PL"),
            PostalCode = "05-820",
            City = "Piastow",
            Street = "ul. St. Bodycha",
            StreetNumber = "97",
            GpsLatitude = 52.180876508945325,
            GpsLongitude = 20.864602669100716
        };

        public static readonly Address Address4Germany = new()
        {
            Id = 4,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            CountryId = 84,
            PostalCode = "13435",
            City = "Berlin",
            Street = "Wallenroder Strasse",
            StreetNumber = "7",
            GpsLatitude = 52.6243741245117,
            GpsLongitude = 13.337297904503853
        };

        public static readonly Address Address5Germany = new()
        {
            Id = 5,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            CountryId = CountrySeeder.GetCountryId("DE"),
            PostalCode = "81677",
            City = "Munich",
            Street = "Kronstadter Str",
            StreetNumber = "30",
            GpsLatitude = 48.141578533011334,
            GpsLongitude = 11.648666754927092
        };

        public static readonly Address Address6Hungary = new()
        {
            Id = 6,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            CountryId = CountrySeeder.GetCountryId("HU"),
            PostalCode = "1097",
            City = "Budapest",
            Street = "Feherakac u.",
            StreetNumber = "821",
            GpsLatitude = 47.469941092986005,
            GpsLongitude = 19.109420384400256
        };

        public static readonly Address Address7Czech = new()
        {
            Id = 7,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            CountryId = CountrySeeder.GetCountryId("CZ"),
            PostalCode = "17000",
            City = "Prague",
            Street = "Argentinska",
            StreetNumber = "516",
            Apartment = "40",
            GpsLatitude = 50.12465893170012,
            GpsLongitude = 14.439680032281526
        };

        public static readonly Address Address8France = new()
        {
            Id = 8,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            CountryId = CountrySeeder.GetCountryId("FR"),
            PostalCode = "06200",
            City = "Nice",
            Street = "Pass. du Fret",
            GpsLatitude = 43.66614093758796,
            GpsLongitude = 7.202632419942202
        };

        public static readonly Address Address9France = new()
        {
            Id = 9,
            Status = DbEntityStatus.Active,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            UpdateUserId = UserSeeder.ServiceUser.Id,
            CountryId = CountrySeeder.GetCountryId("FR"),
            PostalCode = "95540",
            City = "Méry-sur-Oise",
            Street = "Imp. du Château",
            StreetNumber = "2",
            GpsLatitude = 49.06445026229937,
            GpsLongitude = 2.185745049028276
        };

        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<Address>().HasData(Address1Poland);
            builder.Entity<Address>().HasData(Address2Poland);
            builder.Entity<Address>().HasData(Address3Poland);
            builder.Entity<Address>().HasData(Address4Germany);
            builder.Entity<Address>().HasData(Address5Germany);
            builder.Entity<Address>().HasData(Address6Hungary);
            builder.Entity<Address>().HasData(Address7Czech);
            builder.Entity<Address>().HasData(Address8France);
            builder.Entity<Address>().HasData(Address9France);
        }
    }
}
