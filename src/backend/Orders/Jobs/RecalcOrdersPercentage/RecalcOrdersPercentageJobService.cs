using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Orders.Jobs.CreateDeliveries.Models;

namespace Orders.Jobs.RecalcOrdersPercentage
{
    public class RecalcOrdersPercentageJobService
    {
        private readonly AppDbContext _appDbContext;

        public RecalcOrdersPercentageJobService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> CalculateAndUpdatePercentage(Order order)
        {
            int totalQuantity = order.OrderProducts!.Sum(op => op.Quantity);
            int deliveredQuantity = order.Deliveries!.Where(del => del.IsDelivered).Sum(d => d.DeliveryProducts!.Sum(dp => dp.Quantity));

            int percentage = (int)Math.Round((double)(deliveredQuantity * 100) / totalQuantity);

            if (percentage >= 100)
            {
                order.Status = DbEntityStatus.Inactive;
            }

            order.Percentage = percentage;

            await _appDbContext.SaveChangesAsync();

            return percentage;
        }
    }
}
