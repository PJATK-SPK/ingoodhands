using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.OrdersActions.OrdersCancel;
using Orders.Actions.OrdersActions.OrdersGetSingle;
using Orders.Actions.OrdersActions.OrdersSetAsDelivered;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly OrdersGetSingleAction _ordersGetSingleAction;
    private readonly OrdersCancelAction _ordersCancelAction;
    private readonly OrdersSetAsDeliveredAction _setAsDeliveredAction;

    public OrdersController(
        OrdersGetSingleAction ordersGetSingleAction,
        OrdersCancelAction ordersCancelAction,
        OrdersSetAsDeliveredAction setAsDeliveredAction)
    {
        _ordersGetSingleAction = ordersGetSingleAction;
        _ordersCancelAction = ordersCancelAction;
        _setAsDeliveredAction = setAsDeliveredAction;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(string id) => await _ordersGetSingleAction.Execute(id);

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> Cancel(string id) => await _ordersCancelAction.Execute(id);

    [HttpPost("{orderId}/delivery/{deliveryId}/set-as-delivered")]
    public async Task<ActionResult> SetDeliveryAsDelivered(string orderId, string deliveryId)
        => await _setAsDeliveredAction.Execute(orderId, deliveryId);
}
