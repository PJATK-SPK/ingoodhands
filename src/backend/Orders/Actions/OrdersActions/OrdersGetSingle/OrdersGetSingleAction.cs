using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.OrdersActions.OrdersGetSingle
{
    public class OrdersGetSingleAction
    {
        private readonly OrdersGetSingleService _ordersGetSingleService;

        public OrdersGetSingleAction(OrdersGetSingleService ordersGetSingleService)
        {
            _ordersGetSingleService = ordersGetSingleService;
        }

        public async Task<OkObjectResult> Execute(string id)
        {
            var result = await _ordersGetSingleService.Execute(id);

            return new OkObjectResult(result);
        }
    }
}
