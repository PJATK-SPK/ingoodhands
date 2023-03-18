using Core.Database.Models.Core;

namespace Orders.Jobs.CreateDeliveries.Models
{
    public class CreateDeliveriesJobOrderRemainderProduct
    {
        public OrderProduct OrderProduct { get; set; } = default!;
        public int Quantity { get; set; }
        public Product Product => OrderProduct.Product!;
    }
}
