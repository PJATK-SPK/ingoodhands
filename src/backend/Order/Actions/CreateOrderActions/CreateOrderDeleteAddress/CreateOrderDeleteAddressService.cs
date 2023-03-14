using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddress;

namespace Orders.Actions.CreateOrderActions.CreateOrderDeleteAddress
{
    public class CreateOrderDeleteAddressService
    {
        private readonly AppDbContext _appDbContext;
        private readonly Hashids _hashids;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly ILogger<CreateOrderDeleteAddressService> _logger;

        public CreateOrderDeleteAddressService(
            AppDbContext appDbContext,
            Hashids hashids,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            ILogger<CreateOrderDeleteAddressService> logger)
        {
            _appDbContext = appDbContext;
            _hashids = hashids;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _logger = logger;
        }

        public async Task<CreateOrderDeleteAddressResponse> DeleteAddressById(string encodedAddressId)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);
            var decodedAddressId = _hashids.DecodeSingle(encodedAddressId);

            var dbResult = await _appDbContext.UserAddresses
                .Include(c => c.Address)
                    .ThenInclude(c => c!.Country)
                .SingleOrDefaultAsync(c => c.Status == DbEntityStatus.Active && !c.IsDeletedByUser && c.AddressId == decodedAddressId);

            if (dbResult == null)
            {
                _logger.LogError("Couldn't find address with given addressId in database");
                throw new ItemNotFoundException("Cannot find address you'd like to delete in database. Please try again or contact service administrator.");
            }

            dbResult!.UpdateUserId = currentUser.Id;
            dbResult.Status = DbEntityStatus.Inactive;
            dbResult.IsDeletedByUser = true;

            await _appDbContext.SaveChangesAsync();

            var response = new CreateOrderDeleteAddressResponse()
            {
                Id = _hashids.EncodeLong(dbResult.Address!.Id),
                CountryName = dbResult.Address!.Country!.EnglishName,
                PostalCode = dbResult.Address.PostalCode,
                City = dbResult.Address.City,
                Street = dbResult.Address.Street,
                StreetNumber = dbResult.Address.StreetNumber,
                Apartment = dbResult.Address.Apartment,
                GpsLatitude = dbResult.Address.GpsLatitude,
                GpsLongitude = dbResult.Address.GpsLongitude
            };

            return response;
        }
    }
}
