using Core.Database;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Orders.Actions.OrdersActions.OrdersSetAsDelivered
{
    public class OrdersSetAsDeliveredService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<OrdersSetAsDeliveredService> _logger;

        public OrdersSetAsDeliveredService(
            AppDbContext appDbContext,
            ILogger<OrdersSetAsDeliveredService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task SetAsDelivered(long orderId, long deliveryId, User currentUser)
        {
            var order = await _appDbContext.Orders
                .Include(c => c.Deliveries)
                .SingleOrDefaultAsync(c => c.OwnerUserId == currentUser.Id && c.Id == orderId);

            if (order == null)
            {
                _logger.LogError("Couldn't find Order with id:{orderId} in database", orderId);
                throw new ItemNotFoundException("Sorry we couldn't find that order in database");
            }

            var delivery = order.Deliveries!.SingleOrDefault(c => c.Id == deliveryId);

            if (delivery == null)
            {
                _logger.LogError("Couldn't find delivery with id:{deliveryId} in database", deliveryId);
                throw new ItemNotFoundException("Sorry we couldn't find that delivery in database");
            }

            if (delivery.IsDelivered)
            {
                _logger.LogError("Delivery with id:{deliveryId} is already delivered!", deliveryId);
                throw new ItemNotFoundException("This delivery is already delivered!");
            }

            delivery.IsDelivered = true;
            delivery.UpdatedAt = DateTime.UtcNow;

            await _appDbContext.SaveChangesAsync();
        }
    }
}
