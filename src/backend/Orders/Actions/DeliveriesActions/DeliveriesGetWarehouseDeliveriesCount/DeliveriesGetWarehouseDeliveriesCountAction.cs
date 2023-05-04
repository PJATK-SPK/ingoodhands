using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.DeliveriesActions.DeliveriesGetWarehouseDeliveriesCount
{
    public class DeliveriesGetWarehouseDeliveriesCountAction
    {
        private readonly DeliveriesGetWarehouseDeliveriesCountService _deliveriesGetWarehouseDeliveriesCountService;

        public DeliveriesGetWarehouseDeliveriesCountAction(DeliveriesGetWarehouseDeliveriesCountService deliveriesGetWarehouseDeliveriesCountService)
        {
            _deliveriesGetWarehouseDeliveriesCountService = deliveriesGetWarehouseDeliveriesCountService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var result = await _deliveriesGetWarehouseDeliveriesCountService.GetWarehouseDeliveriesCount();

            return new OkObjectResult(result);
        }
    }
}
