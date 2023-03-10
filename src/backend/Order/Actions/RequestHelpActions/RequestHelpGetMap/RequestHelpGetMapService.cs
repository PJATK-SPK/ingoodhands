using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orders.Actions.RequestHelpActions.RequestHelpGetMap;
using System.Linq.Dynamic.Core;
using Z.EntityFramework.Plus;

namespace Orders.Actions.RequestHelpActions.RequestHelpGetMap
{
    public class RequestHelpGetMapService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<RequestHelpGetMapService> _logger;
        private readonly RoleService _roleService;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public RequestHelpGetMapService(
            AppDbContext appDbContext,
            ILogger<RequestHelpGetMapService> logger,
            RoleService roleService,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService
            )
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _roleService = roleService;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task<RequestHelpGetMapResponse> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUserId = _getCurrentUserService.Execute(auth0UserInfo).Result.Id;
            await _roleService.ThrowIfNoRole(RoleName.Needy, currentUserId);

            var listOfWarehouses = await _appDbContext.Warehouses
                .Include(c => c.Address)
                .Where(c => c.Status == DbEntityStatus.Active).FromCache().ToDynamicListAsync();

            var listOfOrders = await _appDbContext.Orders
                .Include(c => c.Address)
                .Where(c => c.Status == DbEntityStatus.Active && c.OwnerUserId == currentUserId)
                .ToListAsync();

            if (!listOfWarehouses.Any())
            {
                _logger.LogError("Couldn't find any active warehouses in database");
                throw new ItemNotFoundException("Couldn't find active warehouses in database");
            }

            var mappedWarehouses = listOfWarehouses.Select(c => new RequestHelpGetMapWarehouseItemResponse
            {
                Name = c.ShortName,
                GpsLatitude = c.Address.GpsLatitude,
                GpsLongitude = c.Address.GpsLongitude
            }).ToList();

            var mappedOrders = listOfOrders.Select(c => new RequestHelpGetMapOrderItemResponse
            {
                Name = c.Name,
                GpsLatitude = c.Address!.GpsLatitude,
                GpsLongitude = c.Address!.GpsLongitude
            }).ToList();

            var response = new RequestHelpGetMapResponse
            {
                Warehouses = mappedWarehouses,
                Orders = mappedOrders
            };

            return response;
        }
    }
}