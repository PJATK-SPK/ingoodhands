using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Orders.Actions.OrdersActions.OrdersGetSingle
{
    public class OrdersGetSingleService
    {
        private readonly Hashids _hashids;
        private readonly RoleService _roleService;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<OrdersGetSingleService> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public OrdersGetSingleService(
            Hashids hashids,
            RoleService roleService,
            AppDbContext appDbContext,
            ILogger<OrdersGetSingleService> logger,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService)
        {
            _logger = logger;
            _hashids = hashids;
            _roleService = roleService;
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task<OrdersGetSingleResponse> Execute(string id)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = _getCurrentUserService.Execute(auth0UserInfo).Result;
            await _roleService.ThrowIfNoRole(RoleName.Needy, currentUser.Id);

            var decodedOrderId = _hashids.DecodeSingleLong(id);

            var dbOrderResult = await _appDbContext.Orders
                .Include(c => c.Address)
                    .ThenInclude(c => c!.Country)
                .Include(c => c.OrderProducts)!
                    .ThenInclude(c => c.Product)
                .Include(c => c.Deliveries)!
                    .ThenInclude(c => c.DelivererUser)
                .SingleOrDefaultAsync(c => c.OwnerUserId == currentUser.Id && c.Id == decodedOrderId);

            if (dbOrderResult == null)
            {
                _logger.LogError("Couldn't find Order with id:{decodedOrderId} in database", decodedOrderId);
                throw new ItemNotFoundException("Sorry we couldn't find that order in database");
            }

            var productResponse = dbOrderResult!.OrderProducts!.Select(c => new OrdersGetSingleProductResponse
            {
                Name = c.Product!.Name,
                Quantity = c.Quantity,
                Unit = c.Product.Unit.ToString().ToLower(),
            }).ToList();

            var deliveryResponse = dbOrderResult.Deliveries!.Select(d => new OrdersGetSingleDeliveryResponse
            {
                Id = _hashids.EncodeLong(d.Id),
                Name = d.Name,
                CreationDate = d.CreationDate,
                IsDelivered = d.IsDelivered,
                TripStarted = d.TripStarted,
                IsLost = d.IsLost,
                DelivererFullName = d.DelivererUser != null ? d.DelivererUser.FirstName + " " + d.DelivererUser.LastName : null,
                DelivererEmail = d.DelivererUser?.Email,
                DelivererPhoneNumber = d.DelivererUser?.PhoneNumber,
            }).ToList();

            var orderItemResponse = new OrdersGetSingleResponse
            {
                Id = _hashids.EncodeLong(dbOrderResult.Id),
                Name = dbOrderResult.Name,
                Percentage = dbOrderResult.Percentage,
                CreationDate = dbOrderResult.CreationDate,
                CountryName = dbOrderResult.Address?.Country?.EnglishName!,
                GpsLatitude = dbOrderResult.Address!.GpsLatitude,
                GpsLongitude = dbOrderResult.Address.GpsLongitude,
                City = dbOrderResult.Address?.City!,
                PostalCode = dbOrderResult.Address?.PostalCode!,
                FullStreet = StreetFullNameBuilderService.Build(dbOrderResult.Address!),
                Deliveries = deliveryResponse,
                Products = productResponse
            };

            return orderItemResponse;
        }
    }
}