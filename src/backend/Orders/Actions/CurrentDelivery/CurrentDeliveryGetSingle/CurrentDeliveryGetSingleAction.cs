using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.CurrentDelivery.CurrentDeliveryGetSingle
{
    public class CurrentDeliveryGetSingleAction
    {
        private readonly CurrentDeliveryGetSingleService _currentDeliveryGetSingleService;

        public CurrentDeliveryGetSingleAction(CurrentDeliveryGetSingleService currentDeliveryGetSingleService)
        {
            _currentDeliveryGetSingleService = currentDeliveryGetSingleService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var result = await _currentDeliveryGetSingleService.GetSingleDelivery();

            return new OkObjectResult(result);
        }
    }
}