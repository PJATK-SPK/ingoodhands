using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesAssignDelivery;
using System.Linq.Dynamic.Core;
using WebApi.Controllers.Order;

namespace Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesGetList
{
    public class AvailableDeliveriesGetListService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly RoleService _roleService;
        private readonly Hashids _hashIds;
        private readonly ILogger<AvailableDeliveriesAssignDeliveryService> _logger;

        public AvailableDeliveriesGetListService(
            AppDbContext appDbContext,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            RoleService roleService,
            Hashids hashIds,
            ILogger<AvailableDeliveriesAssignDeliveryService> logger)
        {
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _roleService = roleService;
            _hashIds = hashIds;
            _logger = logger;
        }

        public async Task<PagedResult<AvailableDeliveriesGetListResponse>> GetList(int page, int pageSize, string? filter = null)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            await _roleService.ThrowIfNoRole(RoleName.Deliverer, currentUser.Id);

            if (currentUser.WarehouseId == null)
            {
                return new PagedResult<AvailableDeliveriesGetListResponse>()
                {
                    CurrentPage = 1,
                    PageCount = 1,
                    PageSize = 1,
                    Queryable = Array.Empty<AvailableDeliveriesGetListResponse>().AsQueryable(),
                    RowCount = 0
                };
            }

            IQueryable<Delivery> dbResult = _appDbContext.Deliveries
                .Include(c => c.Warehouse)
                    .ThenInclude(c => c!.Address)
                        .ThenInclude(c => c!.Country)
                .Include(c => c.Order)
                .Include(c => c.DeliveryProducts)
                .Where(c => !c.TripStarted && !c.IsLost && c.WarehouseId == currentUser.WarehouseId);

            if (filter != null)
            {
                dbResult = dbResult.Where(u => (u.Name).ToLower().Contains(filter.ToLower()));
            }

            var result = dbResult.PageResult(page, pageSize);

            var mapped = result.Queryable.Select(c => new AvailableDeliveriesGetListResponse
            {
                Id = _hashIds.EncodeLong(c.Id),
                DeliveryName = c.Name,
                OrderName = c.Order!.Name,
                WarehouseCountryEnglishName = c.Warehouse!.Address!.Country!.EnglishName,
                WarehouseName = c.Warehouse.ShortName,
                WarehouseFullStreet = StreetFullNameBuilderService.Build(c.Warehouse.Address),
                CreationDate = c.CreationDate,
                ProductTypesCount = c.DeliveryProducts != null ? c.DeliveryProducts.Count : 0
            }).ToList();

            var response = new PagedResult<AvailableDeliveriesGetListResponse>()
            {
                CurrentPage = result.CurrentPage,
                PageCount = result.PageCount,
                PageSize = result.PageSize,
                Queryable = mapped.AsQueryable(),
                RowCount = result.RowCount
            };

            return response;
        }
    }
}
