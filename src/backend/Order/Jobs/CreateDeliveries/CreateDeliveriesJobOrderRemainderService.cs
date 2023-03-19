using Core.Database.Enums;
using Core.Database.Models.Core;
using Orders.Jobs.CreateDeliveries.Models;

namespace Orders.Jobs.CreateDeliveries
{
    public class CreateDeliveriesJobOrderRemainderService
    {
        public Dictionary<long, CreateDeliveriesJobOrderRemainder> Execute(List<Order> orders)
        {
            var result = new Dictionary<long, CreateDeliveriesJobOrderRemainder>();

            foreach (var order in orders)
            {
                var remainder = new CreateDeliveriesJobOrderRemainder
                {
                    Order = order,
                    Products = GenerateProducts(order)
                };

                if (!remainder.Products.Any())
                    continue;

                result.Add(order.Id, remainder);
            }

            return result;
        }

        public void Update(CreateDeliveriesJobOrderRemainder remainder)
        {
            remainder.Products = GenerateProducts(remainder.Order);
        }

        private static List<CreateDeliveriesJobOrderRemainderProduct> GenerateProducts(Order order)
        {
            var result = new List<CreateDeliveriesJobOrderRemainderProduct>();

            foreach (var orderProduct in order.OrderProducts!)
            {
                var deliveryProducts = order.Deliveries!
                    .Where(c => !c.IsLost && c.Status == DbEntityStatus.Active)
                    .SelectMany(c => c.DeliveryProducts!)
                    .Where(c => c.ProductId == orderProduct.ProductId)
                    .ToList()!;

                var providedQty = deliveryProducts.Sum(c => c.Quantity);
                var leftQty = orderProduct.Quantity - providedQty;

                if (leftQty <= 0)
                    continue;

                var item = new CreateDeliveriesJobOrderRemainderProduct
                {
                    OrderProduct = orderProduct,
                    Quantity = leftQty,
                };

                result.Add(item);
            }
            return result;
        }
    }
}
