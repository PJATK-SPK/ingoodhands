using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("warehouses")]
public class WarehousesController : ControllerBase
{

    public class DeleteMeWarehouse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

    [HttpGet]
    public async Task<ActionResult> GetWarehouses()
    {
        var result = new List<DeleteMeWarehouse>
        {
            new DeleteMeWarehouse{Id="n8530v", Name="PL001"},
            new DeleteMeWarehouse{Id="n8630v", Name="PL002"},
            new DeleteMeWarehouse{Id="n853v", Name="PL003"},
        };

        return await Task.FromResult(Ok(result));
    }
}
