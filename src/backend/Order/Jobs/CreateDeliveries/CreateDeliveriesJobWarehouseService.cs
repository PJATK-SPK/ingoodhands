using Core.Database.Models.Core;
using Orders.Jobs.CreateDeliveries.Models;

namespace Orders.Jobs.CreateDeliveries
{
    public class CreateDeliveriesJobWarehouseService
    {
        private readonly CreateDeliveriesJobOrderRemainderService _remainderService;

        public CreateDeliveriesJobWarehouseService(CreateDeliveriesJobOrderRemainderService remainderService)
        {
            _remainderService = remainderService;
        }

        public List<Delivery> AddDeliveriesToOrder(Order order, CreateDeliveriesJobOrderRemainder remainder, List<Stock> stocks)
        {
            var result = new List<Delivery>();

            var items = CreateItems(stocks);

            // Petla:
            // 1. Wygenerowac deliverke
            // 2, Update remainder

            return result;
        }

        private Dictionary<long, List<Stock>> CreateItems(List<Stock> stocks)
        {
            var result = new Dictionary<long, List<Stock>>();

            stocks.ForEach(stock =>
            {
                if (result.ContainsKey(stock.Id))
                    result[stock.Id].Add(stock);
                else
                    result.Add(stock.Id, new List<Stock> { stock });
            });

            return result;
        }
    }
}
