using Core.Database;
using Core.Exceptions;
using Core.Database.Enums;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orders.Actions.DeliveriesActions.DeliveriesGetSingle;
using Core.Services;

namespace Orders.Actions.DeliveriesActions.DeliveriesPickup
{
    public class DeliveriesPickupAction
    {
        private readonly AppDbContext _appDbContext;
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;
        private readonly ILogger<DeliveriesPickupAction> _logger;
        private readonly NotificationService _notificationService;

        public DeliveriesPickupAction(
            AppDbContext appDbContext,
            Hashids hashids,
            ILogger<DeliveriesPickupAction> logger,
            RoleService roleService,
            NotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _hashids = hashids;
            _logger = logger;
            _roleService = roleService;
            _notificationService = notificationService;
        }
        public async Task<OkObjectResult> Execute(string id)
        {
            await _roleService.ThrowIfNoRole(RoleName.WarehouseKeeper);

            var decodedDeliveryId = _hashids.DecodeSingleLong(id);
            var dbResult = await _appDbContext.Deliveries
                .Include(c => c.Warehouse)
                    .ThenInclude(c => c!.Users)
                .SingleOrDefaultAsync(c => c.Id == decodedDeliveryId && c.Status == DbEntityStatus.Active);

            if (dbResult == null)
            {
                _logger.LogError("Couldn't find Delivery with id:{decodedDeliveryId} in database", decodedDeliveryId);
                throw new ItemNotFoundException("Sorry we couldn't find that delivery in database");
            }

            if (dbResult.DelivererUserId == null)
            {
                _logger.LogError("Delivery with id:{decodedDeliveryId} has no assigned deliverer and user tried to start trip.", decodedDeliveryId);
                throw new ItemNotFoundException("You cant start trip of this delivery, because there is no deliverer assigned!");
            }

            dbResult!.TripStarted = true;
            await _appDbContext.SaveChangesAsync();

            var warehouseKeepers = dbResult!.Warehouse!.Users;

            foreach (var warehouseKeeper in warehouseKeepers!)
            {
                await _notificationService.AddAsync(warehouseKeeper.Id, $"Time to pepare delivery: {dbResult.Name}!");
            }

            return new OkObjectResult(new { Status = "OK" });
        }
    }
}
