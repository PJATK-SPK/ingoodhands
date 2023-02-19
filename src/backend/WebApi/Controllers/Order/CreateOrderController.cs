using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("create-order")]
public class CreateOrderController : ControllerBase
{
    internal class DeleteMeGetStocksItemResponse
    {
        public string ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Unit { get; set; } = default!;
    }
    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize)
    {
        var appDbContextProducts = new List<DeleteMeGetStocksItemResponse>
        {
            new DeleteMeGetStocksItemResponse
            {
                ProductId="vb1232fe",
                ProductName = "Milk",
                Quantity = 112,
                Unit="l",
            },
            new DeleteMeGetStocksItemResponse
            {
                ProductId="12ujh5643",
                ProductName = "Rice",
                Quantity = 150,
                Unit="kg",
            }
        }.AsQueryable();

        var result = appDbContextProducts.PageResult(page, pageSize);

        return await Task.FromResult(Ok(result));
    }
}
