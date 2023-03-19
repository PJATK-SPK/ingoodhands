using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.OrdersActions.OrdersCancel;
using Orders.Actions.OrdersActions.OrdersGetSingle;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly OrdersGetSingleAction _ordersGetSingleAction;
    private readonly OrdersCancelAction _ordersCancelAction;

    public OrdersController(OrdersGetSingleAction ordersGetSingleAction, OrdersCancelAction ordersCancelAction)
    {
        _ordersGetSingleAction = ordersGetSingleAction;
        _ordersCancelAction = ordersCancelAction;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(string id) => await _ordersGetSingleAction.Execute(id);

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> Cancel(string id) => await _ordersCancelAction.Execute(id);

    [HttpPost("{orderId}/delivery/{deliveryId}/set-as-delivered")]
    public async Task<ActionResult> SetDeliveryAsDelivered(string orderId, string deliveryId)
    {
        // ustawiasz delivery.IsDelivered =1
        return await Task.FromResult(Ok(new { Message = "OK" }));
    }
}
