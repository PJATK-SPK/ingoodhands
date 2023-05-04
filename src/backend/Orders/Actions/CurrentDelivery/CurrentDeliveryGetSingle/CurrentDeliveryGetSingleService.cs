using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Orders.Actions.CurrentDelivery.CurrentDeliveryGetSingle
{
    public class CurrentDeliveryGetSingleService
    {
        private readonly Hashids _hashids;
        private readonly RoleService _roleService;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CurrentDeliveryGetSingleService> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public CurrentDeliveryGetSingleService(
            Hashids hashids,
            RoleService roleService,
            AppDbContext appDbContext,
            ILogger<CurrentDeliveryGetSingleService> logger,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService)
        {
            _hashids = hashids;
            _roleService = roleService;
            _appDbContext = appDbContext;
            _logger = logger;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task<CurrentDeliveryGetSingleResponse> GetSingleDelivery()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = _getCurrentUserService.Execute(auth0UserInfo).Result;
            await _roleService.ThrowIfNoRole(RoleName.Deliverer, currentUser.Id);

            var dbresult = _appDbContext.Deliveries
                .Include(c => c.DelivererUser)
                .Include(c => c.Order)
                     .ThenInclude(c => c!.OwnerUser)
                .Include(c => c.Order)
                    .ThenInclude(c => c!.Address)
                        .ThenInclude(c => c!.Country)
                .Include(c => c.Warehouse)
                    .ThenInclude(c => c!.Address)
                        .ThenInclude(c => c!.Country)
                .Include(c => c.DeliveryProducts)!
                    .ThenInclude(c => c.Product)
                .FirstOrDefault(c => c.DelivererUserId == currentUser.Id && !c.IsLost && !c.Order!.IsCanceledByUser && !c.IsDelivered);

            if (dbresult == null)
            {
                _logger.LogError("Couldn't find Delivery with delivererId:{delivererId} in database", currentUser.Id);
                throw new ItemNotFoundException("Sorry we couldn't find that delivery in database");
            }

            var productResponse = dbresult!.DeliveryProducts!.Select(c => new CurrentDeliveryGetSingleProductResponse
            {
                Name = c.Product!.Name,
                Quantity = c.Quantity,
                Unit = c.Product.Unit.ToString().ToLower(),
            }).ToList();

            var warehouseLocationResponse = new CurrentDeliveryGetSingleLocationResponse
            {
                CountryName = dbresult.Warehouse!.Address!.Country!.EnglishName,
                GpsLatitude = dbresult.Warehouse.Address.GpsLatitude,
                GpsLongitude = dbresult.Warehouse.Address.GpsLongitude,
                City = dbresult.Warehouse.Address.City,
                PostalCode = dbresult.Warehouse.Address.PostalCode,
                FullStreet = StreetFullNameBuilderService.Build(dbresult.Warehouse.Address),
            };

            var orderLocationResponse = new CurrentDeliveryGetSingleLocationResponse
            {
                CountryName = dbresult.Order!.Address!.Country!.EnglishName,
                GpsLatitude = dbresult.Order.Address!.GpsLatitude,
                GpsLongitude = dbresult.Order.Address.GpsLongitude,
                City = dbresult.Order.Address.City,
                PostalCode = dbresult.Order.Address.PostalCode,
                FullStreet = StreetFullNameBuilderService.Build(dbresult.Order.Address),
            };

            var response = new CurrentDeliveryGetSingleResponse
            {
                Id = _hashids.EncodeLong(dbresult.Id),
                WarehouseName = dbresult.Warehouse.ShortName,
                DeliveryName = dbresult.Name,
                OrderName = dbresult.Order.Name,
                IsDelivered = dbresult.IsDelivered,
                IsLost = dbresult.IsLost,
                TripStarted = dbresult.TripStarted,
                DelivererFullName = dbresult.DelivererUser!.FirstName + " " + dbresult.DelivererUser.LastName,
                NeedyFullName = dbresult.Order.OwnerUser!.FirstName + " " + dbresult.Order.OwnerUser.LastName,
                NeedyPhoneNumber = dbresult.Order.OwnerUser.PhoneNumber!,
                NeedyEmail = dbresult.Order.OwnerUser.Email,
                WarehouseLocation = warehouseLocationResponse,
                OrderLocation = orderLocationResponse,
                CreationDate = dbresult.CreationDate,
                Products = productResponse
            };

            return response;
        }
    }
}
