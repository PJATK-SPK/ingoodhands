using Core.Database;
using Core.Database.Enums;
using Core.Services;
using Core.Setup.Auth0;
using Microsoft.EntityFrameworkCore;

namespace Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesHasActiveDelivery
{
    public class AvailableDeliveriesHasActiveDeliveryService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly RoleService _roleService;

        public AvailableDeliveriesHasActiveDeliveryService(
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

        public async Task<AvailableDeliveriesHasActiveDeliveryResponse> HasActiveDelivery()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            await _roleService.ThrowIfNoRole(RoleName.Deliverer, currentUser.Id);

            var hasActiveDelivery = await _appDbContext.Deliveries.AnyAsync(c => !c.TripStarted & c.DelivererUserId == currentUser.Id && !c.IsDelivered);

            return new AvailableDeliveriesHasActiveDeliveryResponse
            {
                Result = hasActiveDelivery,
            };
        }
    }
}
