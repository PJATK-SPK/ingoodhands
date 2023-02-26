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
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;
        private readonly ILogger<GetWarehousesService> _logger;

        public GetMyDonationDetailsService(
            AppDbContext appDbContext,
            RoleService roleService,
            Hashids hashids,
            ILogger<GetWarehousesService> logger
            )
        {
            _appDbContext = appDbContext;
            _roleService = roleService;
            _hashids = hashids;
            _logger = logger;
        }

        public async Task<GetMyDonationDetailsResponse> GetMyDonationDetails(string id)
        {
            await _roleService.ThrowIfNoRole(RoleName.Donor);
            var decodedDonationId = _hashids.DecodeSingleLong(id);

            var donationById = await _appDbContext.Donations
                .Include(c => c.Products)!
                    .ThenInclude(c => c.Product)
                .Include(c => c.Warehouse)
                    .ThenInclude(c => c!.Address)
                        .ThenInclude(c => c!.Country)
                .SingleOrDefaultAsync(c => c.Id == decodedDonationId);

            if (donationById == null)
            {
                _logger.LogError("Couldn't find donation in database");
                throw new ItemNotFoundException("Sorry there seems to be a problem with our service");
            }
            if (!donationById.Products!.Any())
            {
                _logger.LogError("Couldn't find any active products in database");
                throw new ItemNotFoundException("Sorry there seems to be a problem with our service");
            }

            var productResponse = donationById.Products!.Select(c => new GetMyDonationDetailsProductResponse
            {
                Name = c.Product!.Name,
                Quantity = c.Quantity,
                Unit = c.Product.Unit.ToString().ToLower()
            }).ToList();

            if (donationById.Warehouse == null)
            {
                _logger.LogError("Couldn't find warehouse in database");
                throw new ApplicationErrorException("Sorry there seems to be a problem with our service");
            }

            var response = new GetMyDonationDetailsResponse
            {
                Id = _hashids.EncodeLong(donationById.Id),
                Name = donationById.Name,
                CreationDate = donationById.CreationDate,
                ExpireDate = ExpireDateService.GetExpiredDate4Donation(donationById.CreationDate),
                IsDelivered = donationById.IsDelivered,
                IsExpired = donationById.IsExpired,
                Warehouse = new GetMyDonationDetailsWarehouseResponse
                {
                    Id = _hashids.EncodeLong(donationById.Warehouse.Id),
                    Name = donationById.Warehouse.ShortName,
                    CountryName = donationById.Warehouse.Address!.Country!.EnglishName,
                    GpsLatitude = donationById.Warehouse.Address.GpsLatitude,
                    GpsLongitude = donationById.Warehouse.Address.GpsLongitude,
                    City = donationById.Warehouse.Address.City,
                    PostalCode = donationById.Warehouse.Address.PostalCode,
                    Street = StreetFullNameBuilderService.Build(donationById.Warehouse.Address)
                },
                Products = productResponse
            };

            return response;
        }
    }
}
