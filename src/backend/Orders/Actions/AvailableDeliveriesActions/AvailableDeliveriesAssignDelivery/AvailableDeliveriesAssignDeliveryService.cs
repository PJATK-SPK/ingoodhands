﻿using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesAssignDelivery
{
    public class AvailableDeliveriesAssignDeliveryService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly RoleService _roleService;
        private readonly Hashids _hashIds;
        private readonly ILogger<AvailableDeliveriesAssignDeliveryService> _logger;

        public AvailableDeliveriesAssignDeliveryService(
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

        public async Task<OkResult> AssignDelivery(string id)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            await _roleService.ThrowIfNoRole(RoleName.Deliverer, currentUser.Id);

            var encodedDeliveryId = _hashIds.DecodeSingleLong(id);

            var dbResult = await _appDbContext.Deliveries.SingleOrDefaultAsync(c => c.Id == encodedDeliveryId);

            if (dbResult == null)
            {
                _logger.LogError("Couldn't find Delivery with deliveryId:{deliveryId} in database", encodedDeliveryId);
                throw new ItemNotFoundException("Sorry we couldn't find that delivery in database");
            }

            dbResult!.DelivererUser = currentUser;
            dbResult.TripStarted = true;

            await _appDbContext.SaveChangesAsync();

            return new OkResult();
        }
    }
}
