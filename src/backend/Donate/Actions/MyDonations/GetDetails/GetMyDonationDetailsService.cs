using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using Donate.Actions.DonateForm.GetWarehouses;
using Donate.Shared;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;
using Z.EntityFramework.Plus;

namespace Donate.Actions.MyDonations.GetDetails
{
    public class GetMyDonationDetailsService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;
        private readonly ILogger<GetWarehousesService> _logger;

        public GetMyDonationDetailsService(
            AppDbContext appDbContext,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            RoleService roleService,
            Hashids hashids,
            ILogger<GetWarehousesService> logger
            )
        {
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _roleService = roleService;
            _hashids = hashids;
            _logger = logger;
        }

        public async Task<GetMyDonationDetailsResponse> GetMyDonationDetails(string id)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);
            await _roleService.ThrowIfNoRole(RoleName.Donor, currentUser.Id);
            var decodedDonationId = _hashids.DecodeSingleLong(id);


            var donationById = _appDbContext.Donations.SingleOrDefaultAsync(c => c.Id == decodedDonationId
               && c.Status == DbEntityStatus.Active || c.Status == DbEntityStatus.Inactive);

            if (donationById == null)
            {
                _logger.LogError("Couldn't find donation in database");
                throw new ApplicationErrorException("Sorry there seems to be a problem with our service");
            }

            var listOfDonationProducts = await _appDbContext.DonationProducts
                .Include(c => c.Product)
                .Where(c => c.Donation == donationById.Result
                && c.Status == DbEntityStatus.Active).ToListAsync();

            if (!listOfDonationProducts.Any())
            {
                _logger.LogError("Couldn't find any active products in database");
                throw new ApplicationErrorException("Sorry there seems to be a problem with our service");
            }

            var productResponse = listOfDonationProducts.Select(c => new GetMyDonationDetailsProductResponse
            {
                Name = c.Product!.Name,
                Quantity = c.Quantity,
                Unit = c.Product.Unit.ToString()
            }).ToList();

            var warehouseById = await _appDbContext.Warehouses
              .Include(c => c.Address).ThenInclude(c => c!.Country)
              .SingleOrDefaultAsync(c => c.Id == donationById.Result!.WarehouseId);

            if (warehouseById == null)
            {
                _logger.LogError("Couldn't find warehouse in database");
                throw new ApplicationErrorException("Sorry there seems to be a problem with our service");
            }

            var response = new GetMyDonationDetailsResponse
            {
                Id = _hashids.EncodeLong(donationById.Id),
                Name = donationById.Result!.Name,
                CreationDate = donationById.Result.CreationDate,
                ExpireDate = ExpireDateService.GetExpiredDate4Donation(donationById.Result.CreationDate),
                IsDelivered = donationById.Result.IsDelivered,
                IsExpired = donationById.Result.IsExpired,
                Warehouse = new GetMyDonationDetailsWarehouseResponse
                {
                    Id = _hashids.EncodeLong(warehouseById.Id),
                    Name = warehouseById.ShortName,
                    CountryName = warehouseById.Address!.Country!.EnglishName,
                    GpsLatitude = warehouseById.Address.GpsLatitude,
                    GpsLongitude = warehouseById.Address.GpsLongitude,
                    City = warehouseById.Address.City,
                    PostalCode = warehouseById.Address.PostalCode,
                    Street = StreetFullNameBuilderService.Build(warehouseById.Address)
                },
                Products = productResponse
            };

            return response;
        }
    }
}
