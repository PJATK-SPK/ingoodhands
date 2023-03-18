using Core.Database.Models.Core;

namespace Orders.Jobs.CreateDeliveries.Models
{
    public struct CreateDeliveriesJobDbData
    {
        public List<Order> Orders;
        public List<Stock> Stocks;
    }
}
