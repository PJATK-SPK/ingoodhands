using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.OrdersActions.OrdersGetSingle;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly OrdersGetSingleAction _ordersGetSingleAction;

    public OrdersController(OrdersGetSingleAction ordersGetSingleAction)
    {
        _ordersGetSingleAction = ordersGetSingleAction;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(string id) => await _ordersGetSingleAction.Execute(id);

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> Cancel(string id)
    {
        return await Task.FromResult(Ok(new { Message = "OK" }));
    }
}
