using Core.Actions.DonateForm.GetProducts;
using Donate.Actions.DonateForm.GetWarehouses;
using Donate.Actions.DonateForm.PerformDonate;
using Microsoft.AspNetCore.Authorization;
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
    private readonly PerformDonateAction _performDonateAction;


    public DonateFormController(GetWarehousesAction getWarehousesAction, GetProductsAction getProductsService, PerformDonateAction performDonateAction)
    {
        _getWarehousesAction = getWarehousesAction;
        _getProductsAction = getProductsService;
        _performDonateAction = performDonateAction;
    }

    [HttpGet("warehouses")]
    public async Task<ActionResult> GetWarehouses() => await _getWarehousesAction.Execute();

    [HttpGet("products")]
    public async Task<ActionResult> GetProducts() => await _getProductsAction.Execute();

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Donate([FromBody] PerformDonatePayload payload)
        => await _performDonateAction.Execute(payload);
}
