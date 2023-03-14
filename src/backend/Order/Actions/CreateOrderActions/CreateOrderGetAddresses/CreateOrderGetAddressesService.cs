using Core.Database;
using Core.Database.Enums;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.EntityFrameworkCore;

namespace Orders.Actions.CreateOrderActions.CreateOrderGetAddresses
{
    public class CreateOrderGetAddressesService
    {
        private readonly AppDbContext _appDbContext;
        private readonly Hashids _hashids;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public CreateOrderGetAddressesService(AppDbContext appDbContext, Hashids hashids, ICurrentUserService currentUserService, GetCurrentUserService getCurrentUserService)
        {
            _appDbContext = appDbContext;
            _hashids = hashids;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task<List<CreateOrderGetAddressesItemResponse>> GetActiveAddresses()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            var listOfActiveUserAddresses = await _appDbContext.UserAddresses
                .Include(c => c.Address)
                    .ThenInclude(c => c.Country)
                .Where(c => c.Status == DbEntityStatus.Active && c.UserId == currentUser.Id)
                .ToListAsync();

            var response = listOfActiveUserAddresses.Select(c => new CreateOrderGetAddressesItemResponse
            {
                Id = _hashids.EncodeLong(c.Id),
                CountryName = c.Address!.Country!.EnglishName,
                PostalCode = c.Address.PostalCode,
                City = c.Address.City,
                Street = c.Address.Street,
                StreetNumber = c.Address.StreetNumber,
                Apartment = c.Address.Apartment,
                GpsLatitude = c.Address.GpsLatitude,
                GpsLongitude = c.Address.GpsLongitude
            }).ToList();

            return response;
        }
    }
}
