using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Orders.Actions.DeliveriesActions.DeliveriesSetLost
{
    public class DeliveriesSetLostAction
    {
        private readonly AppDbContext _appDbContext;
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;
        private readonly ILogger<DeliveriesSetLostAction> _logger;

        public DeliveriesSetLostAction(AppDbContext appDbContext, Hashids hashids, ILogger<DeliveriesSetLostAction> logger, RoleService roleService)
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

            if (dbResult.DelivererUserId == null)
            {
                _logger.LogError("Delivery with id:{decodedDeliveryId} has no assigned deliverer and user tried to set is as lost.", decodedDeliveryId);
                throw new ItemNotFoundException("You cant set this delivery as lost, because there is no deliverer assigned!");
            }

            dbResult!.IsLost = true;
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult(new { Status = "OK" });
        }
    }
}
