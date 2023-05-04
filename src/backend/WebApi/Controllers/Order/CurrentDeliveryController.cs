using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.CurrentDelivery.CurrentDeliveryGetSingle;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("current-delivery")]
public class CurrentDeliveryController : ControllerBase
{
    private readonly CurrentDeliveryGetSingleAction _currentDeliveryGetSingleAction;

    public CurrentDeliveryController(CurrentDeliveryGetSingleAction currentDeliveryGetSingleAction)
    {
        _currentDeliveryGetSingleAction = currentDeliveryGetSingleAction;
    }

    [HttpGet()]
    public async Task<ActionResult> GetSingle() => await _currentDeliveryGetSingleAction.Execute();
}
