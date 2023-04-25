using Core.Actions.WarehouseName.GetWarehouseName;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.DeliveriesActions.DeliveriesGetList;
using Orders.Actions.DeliveriesActions.DeliveriesGetSingle;
using System.Linq.Dynamic.Core;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("deliveries")]
public class DeliveriesController : ControllerBase
{
    private readonly GetWarehouseNameAction _getWarehouseNameAction;
    private readonly DeliveriesGetListAction _deliveriesGetListAction;
    private readonly DeliveriesGetSingleAction _deliveriesGetSingleAction;

    public DeliveriesController(
        GetWarehouseNameAction getWarehouseNameAction,
        DeliveriesGetListAction deliveriesGetListAction,
        DeliveriesGetSingleAction deliveriesGetSingleAction)
    {
        _getWarehouseNameAction = getWarehouseNameAction;
        _deliveriesGetListAction = deliveriesGetListAction;
        _deliveriesGetSingleAction = deliveriesGetSingleAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize, string? filter) => await _deliveriesGetListAction.Execute(page, pageSize, filter);

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(string id) => await _deliveriesGetSingleAction.Execute(id);

    [HttpPost("{id}/pickup")]
    public async Task<ActionResult> Pickup(string id)
    {
        // ustawiasz tripstarted =1
        return await Task.FromResult(Ok(new { Message = "OK" }));
    }

    [HttpGet("warehouse-name")]
    public async Task<ActionResult> GetWarehouseName() => await _getWarehouseNameAction.Execute();

    [HttpPost("{id}/set-lost")]
    public async Task<ActionResult> SetLost(string id)
    {
        // ustawiasz islost =1
        return await Task.FromResult(Ok(new { Message = "OK" }));
    }
}
