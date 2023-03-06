using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Order.Actions.WarehousesActions.GetWarehousesList;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("warehouses")]
public class WarehousesController : ControllerBase
{
    private readonly WarehousesGetListAction _warehousesGetListAction;

    public WarehousesController(WarehousesGetListAction warehousesGetListAction)
    {
        _warehousesGetListAction = warehousesGetListAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetWarehouses() => Ok(await _warehousesGetListAction.Execute());
}
