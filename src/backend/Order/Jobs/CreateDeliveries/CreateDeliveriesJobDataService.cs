using Core.Database;
using Core.Database.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Jobs.CreateDeliveries.Models;

namespace Orders.Jobs.CreateDeliveries
{
    public class CreateDeliveriesJobDataService
    {
        private readonly AppDbContext _context;

        public CreateDeliveriesJobDataService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CreateDeliveriesJobDbData> Fetch()
        {
            var orders = _context.Orders
                .Where(c => c.Status == DbEntityStatus.Active && !c.IsCanceledByUser && c.Percentage != 100)
                .Include(c => c.OrderProducts!).ThenInclude(c => c.Product)
                .Include(c => c.Deliveries!).ThenInclude(c => c.DeliveryProducts!).ThenInclude(c => c.Product)
                .ToListAsync();

            var stocks = _context.Stocks
                .Where(c => c.Status == DbEntityStatus.Active)
                .Include(c => c.Product)
                .ToListAsync();

            await Task.WhenAll(orders, stocks);

            return new CreateDeliveriesJobDbData
            {
                Orders = orders.Result,
                Stocks = stocks.Result,
            };
        }
    }
}
