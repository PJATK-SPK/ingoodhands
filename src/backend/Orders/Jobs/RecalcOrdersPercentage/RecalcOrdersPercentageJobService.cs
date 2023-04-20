using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace Orders.Jobs.RecalcOrdersPercentage
{
    public class RecalcOrdersPercentageJobService
    {
        public int CalculateAndUpdatePercentage(Order order)
        {
            int totalQuantity = order.OrderProducts!.Sum(op => op.Quantity);
            int deliveredQuantity = order.Deliveries!.Where(del => del.IsDelivered).Sum(d => d.DeliveryProducts!.Sum(dp => dp.Quantity));

            int percentage = (int)Math.Round((double)(deliveredQuantity * 100) / totalQuantity);

            if (percentage >= 100)
            {
                order.IsFinished = true;
            }

            order.Percentage = percentage;

            return percentage;
        }
    }
}
