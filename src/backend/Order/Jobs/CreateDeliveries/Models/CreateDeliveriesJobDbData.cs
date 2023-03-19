using Core.Database.Models.Core;

namespace Orders.Jobs.CreateDeliveries.Models
{
    public struct CreateDeliveriesJobDbData
    {
        public List<Order> Orders { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}
