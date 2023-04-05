using Core.Actions.WarehouseName.GetWarehouseName;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.StocksActions.StocksGetList;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("stocks")]
public class StocksController : ControllerBase
{
    private readonly StocksGetListAction _stocksGetListAction;
    private readonly GetWarehouseNameAction _getWarehouseNameAction;

    public StocksController(StocksGetListAction stocksGetListAction, GetWarehouseNameAction getWarehouseNameAction)
    {
        _stocksGetListAction = stocksGetListAction;
        _getWarehouseNameAction = getWarehouseNameAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize) => await _stocksGetListAction.Execute(page, pageSize);


    [HttpGet("warehouse-name")]
    public async Task<ActionResult> GetWarehouseName() => await _getWarehouseNameAction.Execute();
}
