using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace Orders.Actions.DeliveriesActions.DeliveriesGetSingle
{
    public class DeliveriesGetSingleService
    {
        private readonly AppDbContext _appDbContext;
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;
        private readonly ILogger<DeliveriesGetSingleService> _logger;


        public DeliveriesGetSingleService(AppDbContext appDbContext, RoleService roleService, Hashids hashids, ILogger<DeliveriesGetSingleService> logger)
        {
            _appDbContext = appDbContext;
            _roleService = roleService;
            _hashids = hashids;
            _logger = logger;
        }

        public async Task<DeliveriesGetSingleResponse> GetSingle(string id)
        {
            await _roleService.ThrowIfNoRole(RoleName.WarehouseKeeper);

            var decodedDeliveryId = _hashids.DecodeSingleLong(id);

            var dbResult = await _appDbContext.Deliveries
                .Include(c => c.Order)
                .Include(c => c.Warehouse)
                    .ThenInclude(c => c!.Address)
                        .ThenInclude(c => c!.Country)
                .Include(c => c.DelivererUser)
                .Include(c => c.DeliveryProducts)!
                    .ThenInclude(c => c.Product)
                .SingleOrDefaultAsync(c => c.Id == decodedDeliveryId);

            if (dbResult == null)
            {
                _logger.LogError("Couldn't find Delivery with id:{decodedDeliveryId} in database", decodedDeliveryId);
                throw new ItemNotFoundException("Sorry we couldn't find that delivery in database");
            }

            var productResponse = dbResult!.DeliveryProducts!.Select(c => new DeliveriesGetSingleProductResponse
            {
                Name = c.Product!.Name,
                Unit = c.Product.Unit.ToString().ToLower(),
                Quantity = c.Quantity
            }).ToList();

            var response = new DeliveriesGetSingleResponse()
            {
                Id = _hashids.EncodeLong(dbResult.Id),
                DeliveryName = dbResult.Name,
                OrderName = dbResult.Order!.Name,
                IsDelivered = dbResult.IsDelivered,
                IsLost = dbResult.IsLost,
                TripStarted = dbResult.TripStarted,
                DelivererFullName = dbResult.DelivererUser!.FirstName + " " + dbResult.DelivererUser.LastName,
                CountryName = dbResult.Warehouse!.Address!.Country!.EnglishName,
                GpsLatitude = dbResult.Warehouse.Address.GpsLatitude,
                GpsLongitude = dbResult.Warehouse.Address.GpsLongitude,
                City = dbResult.Warehouse.Address.City,
                PostalCode = dbResult.Warehouse.Address.PostalCode,
                FullStreet = StreetFullNameBuilderService.Build(dbResult.Warehouse.Address!),
                CreationDate = dbResult.CreationDate,
                Products = productResponse
            };

            return response;
        }
    }
}
