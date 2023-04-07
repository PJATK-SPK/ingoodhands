using Core.Database.Models.Core;
using Orders.Jobs.CreateDeliveries.Models;

namespace Orders.Jobs.RecalcOrdersPercentage
{
    public class RecalcOrdersPercentageJobService
    {


        public int CalculateAndUpdatePercentage(List<Order> orders, Dictionary<long, CreateDeliveriesJobOrderRemainder> reminders)
        {
            return 0;
        }
    }
}
