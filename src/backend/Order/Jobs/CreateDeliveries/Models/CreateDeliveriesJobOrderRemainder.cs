using Core.Database.Models.Core;

namespace Orders.Jobs.CreateDeliveries.Models
{
    public class CreateDeliveriesJobOrderRemainder
    {
        public Order Order { get; set; } = default!;
        public List<CreateDeliveriesJobOrderRemainderProduct> Products { get; set; } = default!;
    }
}
