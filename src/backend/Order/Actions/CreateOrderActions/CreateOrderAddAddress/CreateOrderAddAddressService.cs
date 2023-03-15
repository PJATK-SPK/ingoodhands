using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using FluentValidation;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Orders.Actions.CreateOrderActions.CreateOrderAddAddress
{
    public class CreateOrderAddAddressService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CreateOrderAddAddressService> _logger;
        private readonly Hashids _hashids;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly CreateOrderAddAddressPayloadValidator _createOrderAddAddressPayloadValidator;

        public CreateOrderAddAddressService(
            AppDbContext appDbContext,
            ILogger<CreateOrderAddAddressService> logger,
            Hashids hashids,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            CreateOrderAddAddressPayloadValidator ceateOrderAddAddressPayloadValidator)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _hashids = hashids;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _createOrderAddAddressPayloadValidator = ceateOrderAddAddressPayloadValidator;
        }

        public async Task<CreateOrderAddAddressResponse> AddAddress(CreateOrderAddAddressPayload payload)
        {
            await _createOrderAddAddressPayloadValidator.ValidateAndThrowAsync(payload);

            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            var country = _appDbContext.Countries.SingleOrDefaultAsync(c => c.EnglishName == payload.CountryName);
            if (country.Result == null)
            {
                _logger.LogError("Couldn't find country by with name: {countryName}", payload.CountryName);
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
                UpdateUserId = currentUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            await _appDbContext.AddAsync(newAddress);

            var newUserAddress = new UserAddress()
            {
                User = currentUser,
                Address = newAddress,
                IsDeletedByUser = false,
                UpdateUserId = currentUser.Id,
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
    }
}