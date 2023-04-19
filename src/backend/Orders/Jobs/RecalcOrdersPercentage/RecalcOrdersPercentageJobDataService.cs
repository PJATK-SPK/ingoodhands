using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace Orders.Jobs.RecalcOrdersPercentage
{
    public class RecalcOrdersPercentageJobDataService
    {
        private readonly AppDbContext _appDbContext;

        public RecalcOrdersPercentageJobDataService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Order>> Fetch()
        {
            var orders = await _appDbContext.Orders
                .Where(c => c.Status == DbEntityStatus.Active && !c.IsCanceledByUser && c.Percentage != 100)
                .Include(c => c.OrderProducts!).ThenInclude(c => c.Product)
                .Include(c => c.Deliveries!).ThenInclude(c => c.DeliveryProducts!).ThenInclude(c => c.Product)
                .Where(c => c.Deliveries!.Any(d => d.IsDelivered && !d.IsLost && d.TripStarted))
                .ToListAsync();

            return orders;
        }
    }
}
