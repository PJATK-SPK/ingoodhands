using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTests.Actions.CreateOrderActionTest.CreateOrderDeleteAddressActionTest
{
    public static class CreateOrderDeleteAddressActionFixture
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

        public static Address CreateAddress(Address addres1, Address address2) => new()
        {
            CountryId = 177,
            PostalCode = addres1.PostalCode,
            City = addres1.City,
            Street = addres1.Street,
            StreetNumber = address2.StreetNumber,
            Apartment = address2.Apartment,
            GpsLatitude = address2.GpsLatitude,
            GpsLongitude = addres1.GpsLongitude,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active
        };

        public static UserAddress CreateUserAddress(User user, Address address) => new()
        {
            User = user,
            Address = address,
            IsDeletedByUser = false,
            UpdateUser = user,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active
        };
    }
}
