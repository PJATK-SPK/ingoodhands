using Donate.Actions.DonateForm.GetWarehouses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Donate;

[EnableCors]
[ApiController]
[Route("donate-form")]
public class DonateFormController : ControllerBase
{
    private readonly GetWarehousesAction _getWarehousesAction;

    public DonateFormController(GetWarehousesAction getWarehousesAction)
    {
        _getWarehousesAction = getWarehousesAction;
    }

    [HttpGet("warehouses")]
    public async Task<ActionResult> GetWarehouses() => await _getWarehousesAction.Execute();


    internal class DeleteMeGetProductsResponse
    {
        public string Id { get; set; } = default!; // ProductId
        public string Name { get; set; } = default!;
        public string Unit { get; set; } = default!;
    }
    [HttpGet("products")]
    public async Task<ActionResult> GetProducts()
    {
        return await Task.FromResult(Ok(new List<DeleteMeGetProductsResponse>
        {
            new DeleteMeGetProductsResponse
            {
               Id = "b786543g",
               Name = "Fruits",
               Unit = "kg"
            },
            new DeleteMeGetProductsResponse
            {
               Id = "754654s",
               Name = "Water",
               Unit = "l"
            },
            new DeleteMeGetProductsResponse
            {
               Id = "127343g",
               Name = "Rice",
               Unit = "kg"
            },
            new DeleteMeGetProductsResponse
            {
               Id = "bi4543g",
               Name = "Milk",
               Unit = "l"
            },
            new DeleteMeGetProductsResponse
            {
               Id = "basg3g",
               Name = "Meat",
               Unit = "kg"
            }
        }));
    }

    public class DeleteMePerformDonatePayloadProduct
    {
        public string Id { get; set; } = default!;
        public int Quantity { get; set; }
    }
    public class DeleteMePerformDonatePayload
    {
        public string WarehouseId { get; set; } = default!;
        public List<DeleteMePerformDonatePayloadProduct> Products { get; set; } = default!;
    }
    public class DeleteMePerformDonateResponse
    {
        public string DonateNumber { get; set; } = default!;
    }
    [HttpPost]
    public async Task<ActionResult> Donate([FromBody] DeleteMePerformDonatePayload payload)
        => await Task.FromResult(Ok(new DeleteMePerformDonateResponse { DonateNumber = "DNT000001" }));
}
