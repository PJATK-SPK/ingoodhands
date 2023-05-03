using Core.Database;
using Core.Database.Enums;
using Core.Services;
using Core.Setup.Auth0;
using Microsoft.EntityFrameworkCore;

namespace Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesCount
{
    public class AvailableDeliveriesCountService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly RoleService _roleService;

        public AvailableDeliveriesCountService(
            AppDbContext appDbContext,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            RoleService roleService)
        {
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _roleService = roleService;
        }

        public async Task<AvailableDeliveriesCountResponse> CountAvailableDeliveries()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            await _roleService.ThrowIfNoRole(RoleName.Deliverer, currentUser.Id);


            if (currentUser.Warehouse == null)
            {
                return new AvailableDeliveriesCountResponse
                {
                    Count = 0,
                };
            }

            var delivererWarehouse = await _appDbContext.Users
                .Include(c => c.Warehouse)
                    .ThenInclude(c => c!.Deliveries)
                .Where(c => c.Id == currentUser.Id)
                .ToListAsync();

            var warehouseId = delivererWarehouse.First().WarehouseId;
            var numberOfDeliveriesInWarehouse = delivererWarehouse
                .Where(c => c.WarehouseId == warehouseId)
                .SelectMany(c => c.Warehouse!.Deliveries!)
                .Count(c => !c.TripStarted && !c.IsLost);

            return new AvailableDeliveriesCountResponse
            {
                Count = numberOfDeliveriesInWarehouse,
            };
        }
    }
}
