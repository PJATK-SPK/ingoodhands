using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.CreateOrderActions.CreateOrderCreateOrder
{
    public class CreateOrderCreateOrderAction
    {
        private readonly CreateOrderCreateOrderService _createOrderCreateOrderService;

        public CreateOrderCreateOrderAction(CreateOrderCreateOrderService createOrderCreateOrderService)
        {
            _createOrderCreateOrderService = createOrderCreateOrderService;
        }

        public async Task<OkObjectResult> Execute(CreateOrderCreateOrderPayload payload)
        {
            var result = await _createOrderCreateOrderService.CreateOrder(payload);

            return new OkObjectResult(result);
        }
    }
}
