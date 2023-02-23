using Donate.Actions.DonateForm.GetProducts;
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
    private readonly GetProductsAction _getProductsAction;

    public DonateFormController(GetWarehousesAction getWarehousesAction, GetProductsAction getProductsService)
    {
        _getWarehousesAction = getWarehousesAction;
        _getProductsAction = getProductsService;
    }

    [HttpGet("warehouses")]
    public async Task<ActionResult> GetWarehouses() => await _getWarehousesAction.Execute();

    [HttpGet("products")]
    public async Task<ActionResult> GetProducts() => await _getProductsAction.Execute();

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
