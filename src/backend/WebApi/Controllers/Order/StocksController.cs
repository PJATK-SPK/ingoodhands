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

    public StocksController(StocksGetListAction stocksGetListAction)
    {
        _stocksGetListAction = stocksGetListAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize) => await _stocksGetListAction.Execute(page, pageSize);
}
