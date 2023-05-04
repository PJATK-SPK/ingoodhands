using Core.Database;
using Core.Database.Enums;
using Core.Services;
using Core.Setup.Auth0;
using Microsoft.EntityFrameworkCore;

namespace Orders.Actions.DeliveriesActions.DeliveriesGetWarehouseDeliveriesCount
{
    public class DeliveriesGetWarehouseDeliveriesCountService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly RoleService _roleService;

        public DeliveriesGetWarehouseDeliveriesCountService(
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

        public async Task<DeliveriesGetWarehouseDeliveriesCountResponse> GetWarehouseDeliveriesCount()
        {

            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            await _roleService.ThrowIfNoRole(RoleName.WarehouseKeeper, currentUser.Id);


            if (currentUser.Warehouse == null)
            {
                return new DeliveriesGetWarehouseDeliveriesCountResponse
                {
                    Count = 0,
                };
            }
            var warehouseKeeperWithWarehouse = await _appDbContext.Users
                .Include(c => c.Warehouse)
                    .ThenInclude(c => c!.Deliveries)
                .Where(c => c.Id == currentUser.Id)
                .ToListAsync();

            var warehouseId = warehouseKeeperWithWarehouse.First().WarehouseId;
            var numberOfDeliveriesInWarehouse = warehouseKeeperWithWarehouse
                .Where(c => c.WarehouseId == warehouseId)
                .SelectMany(c => c.Warehouse!.Deliveries!)
                .Count(c => c.TripStarted && !c.IsLost && !c.IsDelivered);

            return new DeliveriesGetWarehouseDeliveriesCountResponse
            {
                Count = numberOfDeliveriesInWarehouse,
            };
        }
    }
}
