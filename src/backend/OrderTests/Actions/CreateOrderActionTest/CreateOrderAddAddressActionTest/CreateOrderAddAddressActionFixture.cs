using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Seeders;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddresses;

namespace OrdersTests.Actions.CreateOrderActionTest.CreateOrderAddAddressActionTest
{
    public static class CreateOrderAddAddressActionFixture
    {
        public static User CreateUser(string firstName, string lastName) => new()
        {
            Status = DbEntityStatus.Active,
            FirstName = firstName,
            LastName = lastName,
            Email = firstName + "@" + lastName + ".com",
        };

        public static Auth0User CreateAuth0User(User user, int id) => new()
        {
            FirstName = "Auth",
            LastName = "Auth0User" + id,
            Nickname = "Auth0",
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Email = user.Email,
            Identifier = user.FirstName + user.LastName + "GoogleId",
            User = user,
            UserId = user.Id
        };

        public static CreateOrderAddAddressPayload CreatePayload(string countryName) => new()
        {
            Id = "",
            CountryName = countryName,
            PostalCode = "82-420",
            City = "Bachmut",
            Street = "Papieska",
            StreetNumber = "21",
            Apartment = "37",
            GpsLatitude = 1.111,
            GpsLongitude = 2.222
        };

        public static CreateOrderAddAddressPayload CreatePayloadNullStreetValues(string countryName) => new()
        {
            Id = "",
            CountryName = countryName,
            PostalCode = "82-420",
            City = "Bachmut",
            Street = null,
            StreetNumber = null,
            Apartment = null,
            GpsLatitude = 1.111,
            GpsLongitude = 2.222
        };
    }
}
