using Core.Actions.WarehouseName.GetWarehouseName;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.DeliveriesActions.DeliveriesGetList;
using Orders.Actions.DeliveriesActions.DeliveriesGetSingle;
using Orders.Actions.DeliveriesActions.DeliveriesPickup;
using Orders.Actions.DeliveriesActions.DeliveriesSetLost;
using System.Linq.Dynamic.Core;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("deliveries")]
public class DeliveriesController : ControllerBase
{
    private readonly GetWarehouseNameAction _getWarehouseNameAction;
    private readonly DeliveriesPickupAction _deliveriesPickupAction;
    private readonly DeliveriesSetLostAction _deliveriesSetLostAction;
    private readonly DeliveriesGetListAction _deliveriesGetListAction;
    private readonly DeliveriesGetSingleAction _deliveriesGetSingleAction;

    public DeliveriesController(
        GetWarehouseNameAction getWarehouseNameAction,
        DeliveriesGetListAction deliveriesGetListAction,
        DeliveriesGetSingleAction deliveriesGetSingleAction,
        DeliveriesPickupAction deliveriesPickupAction,
        DeliveriesSetLostAction deliveriesSetLostAction)
    {
        _getWarehouseNameAction = getWarehouseNameAction;
        _deliveriesGetListAction = deliveriesGetListAction;
        _deliveriesGetSingleAction = deliveriesGetSingleAction;
        _deliveriesPickupAction = deliveriesPickupAction;
        _deliveriesSetLostAction = deliveriesSetLostAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize, string? filter) => await _deliveriesGetListAction.Execute(page, pageSize, filter);

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(string id) => await _deliveriesGetSingleAction.Execute(id);

    [HttpPost("{id}/pickup")]
    public async Task<ActionResult> Pickup(string id) => await _deliveriesPickupAction.Execute(id);

    [HttpGet("warehouse-name")]
    public async Task<ActionResult> GetWarehouseName() => await _getWarehouseNameAction.Execute();

    [HttpPost("{id}/set-lost")]
    public async Task<ActionResult> SetLost(string id) => await _deliveriesSetLostAction.Execute(id);
}
