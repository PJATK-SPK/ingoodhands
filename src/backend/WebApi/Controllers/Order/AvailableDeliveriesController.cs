using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesAssignDelivery;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesCount;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesGetList;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesHasActiveDelivery;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("available-deliveries")]
public class AvailableDeliveriesController : ControllerBase
{
    private readonly AvailableDeliveriesCountAction _availableDeliveriesCountAction;
    private readonly AvailableDeliveriesHasActiveDeliveryAction _availableDeliveriesHasActiveDeliveryAction;
    private readonly AvailableDeliveriesAssignDeliveryAction _availableDeliveriesAssignDeliveryAction;
    private readonly AvailableDeliveriesGetListAction _availableDeliveriesGetListAction;

    public AvailableDeliveriesController(
        AvailableDeliveriesCountAction availableDeliveriesCountAction,
        AvailableDeliveriesHasActiveDeliveryAction availableDeliveriesHasActiveDeliveryAction,
        AvailableDeliveriesAssignDeliveryAction availableDeliveriesAssignDeliveryAction,
        AvailableDeliveriesGetListAction availableDeliveriesGetListAction)
    {
        _availableDeliveriesCountAction = availableDeliveriesCountAction;
        _availableDeliveriesHasActiveDeliveryAction = availableDeliveriesHasActiveDeliveryAction;
        _availableDeliveriesAssignDeliveryAction = availableDeliveriesAssignDeliveryAction;
        _availableDeliveriesGetListAction = availableDeliveriesGetListAction;
    }

    [HttpGet("count")]
    public async Task<ActionResult> GetWarehouseDeliveriesCount() => await _availableDeliveriesCountAction.Execute();

    [HttpGet("has-active-delivery")]
    public async Task<ActionResult> HasActiveDelivery() => await _availableDeliveriesHasActiveDeliveryAction.Execute();

    [HttpPost("assign-delivery/{deliveryId}")]
    public async Task<ActionResult> AssignDelivery(string deliveryId) => await _availableDeliveriesAssignDeliveryAction.Execute(deliveryId);

    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize, string? filter) => await _availableDeliveriesGetListAction.Execute(page, pageSize, filter);
}
