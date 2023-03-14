using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddress;

namespace Orders.Actions.CreateOrderActions.CreateOrderAddAddresses
{
    public class CreateOrderAddAddressService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CreateOrderAddAddressService> _logger;
        private readonly Hashids _hashids;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public CreateOrderAddAddressService(
            AppDbContext appDbContext,
            ILogger<CreateOrderAddAddressService> logger,
            Hashids hashids,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService
            )
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _hashids = hashids;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task<CreateOrderAddAddressResponse> AddAddress(CreateOrderAddAddressPayload payload)
        {
            if (!ValidatePayload(payload))
            {
                _logger.LogError("Invalid payload");
                throw new ClientInputErrorException("Sorry, we couldn't fetch your new address. Please contact service administrator.");
            }

            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            var country = _appDbContext.Countries.SingleOrDefaultAsync(c => c.EnglishName == payload.CountryName);
            if (country.Result == null)
            {
                _logger.LogError("Couldn't find country by it's EnglishName");
                throw new ItemNotFoundException("Cannot find given country name in database. Please provide valid country name.");
            }

            var newAddress = new Address()
            {
                Country = country.Result,
                PostalCode = payload.PostalCode,
                City = payload.City,
                Street = payload.Street,
                StreetNumber = payload.StreetNumber,
                Apartment = payload.Apartment,
                GpsLatitude = payload.GpsLatitude,
                GpsLongitude = payload.GpsLongitude,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            await _appDbContext.AddAsync(newAddress);

            var newUserAddress = new UserAddress()
            {
                User = currentUser,
                Address = newAddress,
                IsDeletedByUser = false,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            await _appDbContext.AddAsync(newUserAddress);

            await _appDbContext.SaveChangesAsync();

            var response = new CreateOrderAddAddressResponse()
            {
                Id = _hashids.EncodeLong(newAddress.Id),
                CountryName = newAddress.Country!.EnglishName,
                PostalCode = newAddress.PostalCode,
                City = newAddress.City,
                Street = newAddress.Street,
                StreetNumber = newAddress.StreetNumber,
                Apartment = newAddress.Apartment,
                GpsLatitude = newAddress.GpsLatitude,
                GpsLongitude = newAddress.GpsLongitude
            };

            return response;
        }

        private bool ValidatePayload(CreateOrderAddAddressPayload payload)
        {
            if (string.IsNullOrWhiteSpace(payload.CountryName)) return false;
            if (string.IsNullOrWhiteSpace(payload.PostalCode)) return false;
            if (string.IsNullOrWhiteSpace(payload.City)) return false;
            if (payload.GpsLatitude == default(double)) return false;
            if (payload.GpsLongitude == default(double)) return false;

            return true;
        }
    }
}
