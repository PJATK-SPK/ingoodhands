using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("deliveries")]
public class DeliveriesController : ControllerBase
{
    public class DeleteMeResponse1 { public string WarehouseName { get; set; } = default!; }
    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize)
    {
        var appDbContextProducts = new List<DeleteMeResponse1>
        {
            new DeleteMeResponse1
            {
            },
            new DeleteMeResponse1
            {
            }
        }.AsQueryable();

        var result = appDbContextProducts.PageResult(page, pageSize);

        return await Task.FromResult(Ok(result));
    }
}
