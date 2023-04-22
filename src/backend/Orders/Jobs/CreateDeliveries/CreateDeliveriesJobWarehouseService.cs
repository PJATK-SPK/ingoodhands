using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Orders.Jobs.CreateDeliveries.Models;
using Orders.Services.OrderNameBuilder;

namespace Orders.Jobs.CreateDeliveries
{
    public class CreateDeliveriesJobWarehouseService
    {
        private readonly OrderNameBuilderService _orderNameBuilderService;
        private readonly CounterService _counterService;
        private readonly NotificationService _notificationService;

        public CreateDeliveriesJobWarehouseService(
            OrderNameBuilderService orderNameBuilderService,
            CounterService counterService,
            NotificationService notificationService)
        {
            _orderNameBuilderService = orderNameBuilderService;
            _counterService = counterService;
            _notificationService = notificationService;
        }

        public async Task<List<Delivery>> AddDeliveriesToOrder(Order order, CreateDeliveriesJobOrderRemainder remainder, List<Stock> stocks)
        {
            var result = new List<Delivery>();

            var warehousesStocks = ComputeWarehousesStocks(stocks);

            while (AnyWarehouseHasSomethingFromThisOrder(remainder, stocks))
            {
                var delivery = await CreateDeliveryForWarehouseWithMostStock(order, remainder, warehousesStocks);

                order.Deliveries!.Add(delivery);
                CreateDeliveriesJobOrderRemainderService.Update(remainder);
            }

            return result;
        }

        private static Dictionary<long, List<Stock>> ComputeWarehousesStocks(List<Stock> stocks)
        {
            var result = new Dictionary<long, List<Stock>>();

            stocks.ForEach(stock =>
            {
                if (result.TryGetValue(stock.WarehouseId, out var item))
                    item.Add(stock);
                else
                    result.Add(stock.WarehouseId, new List<Stock> { stock });
            });

            return result;
        }

        private static bool AnyWarehouseHasSomethingFromThisOrder(
           CreateDeliveriesJobOrderRemainder remainder,
           List<Stock> stocks)
        {
            var not0OrderProducts = remainder.Products.Where(c => c.Quantity > 0).Select(c => c.OrderProduct.ProductId);
            var not0Stocks = stocks.Where(c => c.Quantity > 0).Select(c => c.ProductId);

            return not0Stocks.Any(c => not0OrderProducts.Contains(c));
        }

        private async Task<Delivery> CreateDeliveryForWarehouseWithMostStock(
            Order order,
            CreateDeliveriesJobOrderRemainder remainder,
            Dictionary<long, List<Stock>> warehousesStocks)
        {
            var warehouseIdWithMostStock = warehousesStocks
                .Select(c => new { WarehouseId = c.Key, Score = GetScoreForWarehouse(remainder, c.Value) })
                .OrderByDescending(c => c.Score)
                .Select(c => c.WarehouseId)
                .First();

            var stocks = warehousesStocks[warehouseIdWithMostStock];
            var warehouse = stocks.First().Warehouse!;
            var warehouseDeliverers = warehouse.Users!.Where(c => c.Roles!.Any(s => s.Role!.Name == RoleName.Deliverer)).ToList();

            if (!warehouseDeliverers.Any())
                throw new ItemNotFoundException($"Cannot create delivery for order {order.Id}, because warehouse {warehouse.Id} has no deliverers!");

            var deliverer = warehouseDeliverers[Random.Shared.Next(warehouseDeliverers.Count)];

            var result = new Delivery
            {
                UpdateUserId = UserSeeder.ServiceUser.Id,
                CreationDate = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active,
                DelivererUserId = deliverer.Id,
                Name = _orderNameBuilderService.Build(await _counterService.GetAndUpdateNextCounter(TableName.Deliveries)),
                Order = order,
                WarehouseId = warehouse.Id,
            };

            result.DeliveryProducts = CreateDeliveryProductsForWarehouse(result, remainder, stocks);

            var warehouseKeepers = warehouse.Users!.Where(c => c.Roles!.Any(r => r.Role!.Name == RoleName.WarehouseKeeper)).ToList();
            foreach (var warehouseKeeper in warehouseKeepers)
            {
                await _notificationService.AddAsync(warehouseKeeper.Id, $"New delivery {result.Name} has been assigned to your warehouse!");
            }

            await _notificationService.AddAsync(deliverer.Id, $"New delivery {result.Name} has been added to your tasks!");

            return result;
        }

        private static long GetScoreForWarehouse(CreateDeliveriesJobOrderRemainder remainder, List<Stock> stocks)
        {
            var result = 0;

            foreach (var remainderProduct in remainder.Products)
            {
                var stock = stocks.SingleOrDefault(c => c.ProductId == remainderProduct.OrderProduct.ProductId);
                if (stock == null)
                    continue;

                result += stock.Quantity;
            }

            return result;
        }

        private static List<DeliveryProduct> CreateDeliveryProductsForWarehouse(
            Delivery delivery,
            CreateDeliveriesJobOrderRemainder remainder,
            List<Stock> stocks)
        {
            var result = new List<DeliveryProduct>();

            foreach (var remainderProduct in remainder.Products)
            {
                var stock = stocks.SingleOrDefault(c => c.ProductId == remainderProduct.OrderProduct.ProductId);
                if (stock == null || stock.Quantity <= 0)
                    continue;

                // == Example 1
                // ORDER: 150
                // STOCK: 200
                // --
                // GET: 150
                // STOCK AFTER: 50

                // == Example 2
                // ORDER: 200
                // STOCK: 120
                // --
                // GET: 120
                // STOCK AFTER: 0

                var qtyToGet =
                    stock.Quantity >= remainderProduct.Quantity ? remainderProduct.Quantity : stock.Quantity;

                var newStockQty =
                    stock.Quantity >= remainderProduct.Quantity ? stock.Quantity - remainderProduct.Quantity : 0;

                stock.Quantity = newStockQty;
                stock.UpdatedAt = DateTime.UtcNow;
                stock.UpdateUserId = UserSeeder.ServiceUser.Id;

                var deliveryProduct = new DeliveryProduct
                {
                    Delivery = delivery,
                    ProductId = remainderProduct.OrderProduct.ProductId,
                    Quantity = qtyToGet,
                    UpdateUserId = UserSeeder.ServiceUser.Id,
                    UpdatedAt = DateTime.UtcNow,
                };

                result.Add(deliveryProduct);
            }

            return result;
        }
    }
}
