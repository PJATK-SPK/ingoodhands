using Core.Actions.WarehouseName.GetWarehouseName;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.DeliveriesActions.DeliveriesGetList;
using Orders.Actions.DeliveriesActions.DeliveriesGetSingle;
using Orders.Actions.DeliveriesActions.DeliveriesPickup;
using Orders.Actions.DeliveriesActions.DeliveriesSetLost;

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

    // Sandro: Analogia do MyDonationsController::GetCountOfNotDelivered. Tutaj sprawdzamy czy pan jest magazynierem i bierymy jego warehouse
    // i patrzymy ile jest wolnych deliverek dla jego warehouseu. Np jak s¹ 2 ordery z których s¹ 4 deliverki (wziete przez deliverera badz nie)
    // to tu zwracamy 4 - czyli tyle samo ile w GetList() tam wyzej
    // UWAGAA !!!!!!!!!! GDY PAN NIE MA WAREHOUSEID ZWRACAMY 0 (ZERO)
    public class DeleteMe1 { public int Count { get; set; } = 1; }
    [HttpGet("warehouse-deliveries-count")]
    public async Task<ActionResult> GetWarehouseDeliveriesCount() => await Task.Run(() => Ok(new DeleteMe1()));
}
