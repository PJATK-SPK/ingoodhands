﻿using Core.Database;
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

        public DeliveriesPickupAction(AppDbContext appDbContext, Hashids hashids, ILogger<DeliveriesPickupAction> logger, RoleService roleService)
        {
            _appDbContext = appDbContext;
            _hashids = hashids;
            _logger = logger;
            _roleService = roleService;
        }
        public async Task<OkObjectResult> Execute(string id)
        {
            await _roleService.ThrowIfNoRole(RoleName.WarehouseKeeper);

            var decodedDeliveryId = _hashids.DecodeSingleLong(id);
            var dbResult = await _appDbContext.Deliveries.SingleOrDefaultAsync(c => c.Id == decodedDeliveryId && c.Status == DbEntityStatus.Active);

            if (dbResult == null)
            {
                _logger.LogError("Couldn't find Delivery with id:{decodedDeliveryId} in database", decodedDeliveryId);
                throw new ItemNotFoundException("Sorry we couldn't find that delivery in database");
            }

            dbResult!.TripStarted = true;
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult(new { Status = "OK" });
        }
    }
}