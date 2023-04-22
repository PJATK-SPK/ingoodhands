using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.OrdersActions.OrdersCancel
{
    public class OrdersCancelAction
    {
        private readonly OrdersCancelService _ordersCancelService;

        public OrdersCancelAction(OrdersCancelService ordersCancelService)
        {
            _ordersCancelService = ordersCancelService;
        }

        public async Task<OkObjectResult> Execute(string id)
        {
            await _ordersCancelService.CancelOrderById(id);

            return new OkObjectResult(new { Message = "OK" });
        }
    }
}
