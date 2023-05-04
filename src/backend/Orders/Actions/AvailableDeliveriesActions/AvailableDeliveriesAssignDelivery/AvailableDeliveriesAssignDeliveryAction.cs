using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesAssignDelivery
{
    public class AvailableDeliveriesAssignDeliveryAction
    {
        private readonly AvailableDeliveriesAssignDeliveryService _availableDeliveriesAssignDeliveryService;

        public AvailableDeliveriesAssignDeliveryAction(AvailableDeliveriesAssignDeliveryService availableDeliveriesAssignDeliveryService)
        {
            _availableDeliveriesAssignDeliveryService = availableDeliveriesAssignDeliveryService;
        }

        public async Task<OkObjectResult> Execute(string id)
        {
            var result = await _availableDeliveriesAssignDeliveryService.AssignDelivery(id);

            return new OkObjectResult(result);
        }
    }
}
