using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
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

        public RequestHelpGetMapService(AppDbContext appDbContext, ILogger<RequestHelpGetMapService> logger, RoleService roleService)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _roleService = roleService;
        }

        public async Task<RequestHelpGetMapResponse> Execute()
        {
            await _roleService.ThrowIfNoRole(RoleName.Needy);

            var listOfWarehouses = await _appDbContext.Warehouses
                .Include(c => c.Address)
                .Where(c => c.Status == DbEntityStatus.Active).FromCache().ToDynamicListAsync();

            var listOfOrders = await _appDbContext.Orders
                .Include(c => c.Address)
                .Where(c => c.Status == DbEntityStatus.Active).ToListAsync();

            if (!listOfWarehouses.Any())
            {
                _logger.LogError("Couldn't find any active warehouses in database");
                throw new ItemNotFoundException("Couldn't find active warehouses in database");
            }

            if (!listOfOrders.Any())
            {
                _logger.LogError("Couldn't find any active orders in database");
                throw new ItemNotFoundException("Couldn't find active orders in database");
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