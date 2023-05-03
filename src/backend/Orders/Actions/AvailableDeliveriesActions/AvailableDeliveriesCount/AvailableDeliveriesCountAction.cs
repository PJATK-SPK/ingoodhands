using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesCount
{
    public class AvailableDeliveriesCountAction
    {
        private readonly AvailableDeliveriesCountService _availableDeliveriesCountService;

        public AvailableDeliveriesCountAction(AvailableDeliveriesCountService availableDeliveriesCountService)
        {
            _availableDeliveriesCountService = availableDeliveriesCountService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var result = await _availableDeliveriesCountService.CountAvailableDeliveries();

            return new OkObjectResult(result);
        }
    }
}