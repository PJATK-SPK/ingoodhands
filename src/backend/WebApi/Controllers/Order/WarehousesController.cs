using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("warehouses")]
public class WarehousesController : ControllerBase
{
    [HttpGet("{warehouses}")]
    public async Task<ActionResult> GetWarehouses()
    {
        var result = new List<string>
        {
            "PL001",
            "PL002",
            "PL003",
        };

        return await Task.FromResult(Ok(result));
    }
}
