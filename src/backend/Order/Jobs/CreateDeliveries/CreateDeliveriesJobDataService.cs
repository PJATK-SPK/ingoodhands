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
            var orders = await _context.Orders
                .Where(c => c.Status == DbEntityStatus.Active && !c.IsCanceledByUser && c.Percentage != 100)
                .Include(c => c.OrderProducts!).ThenInclude(c => c.Product)
                .Include(c => c.Deliveries!).ThenInclude(c => c.DeliveryProducts!).ThenInclude(c => c.Product)
                .ToListAsync();

            var stocks = await _context.Stocks
                .Where(c => c.Status == DbEntityStatus.Active)
                .Include(c => c.Product)
                .Include(c => c.Warehouse!).ThenInclude(c => c.Users!).ThenInclude(c => c.Roles!).ThenInclude(c => c.Role)
                .ToListAsync();

            return new CreateDeliveriesJobDbData
            {
                Orders = orders,
                Stocks = stocks,
            };
        }
    }
}
