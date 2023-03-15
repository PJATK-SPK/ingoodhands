using Core.Actions.DonateForm.GetProducts;
using Core.Services;
using Core.Database.Enums;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddress;
using Orders.Actions.CreateOrderActions.CreateOrderCreateOrder;
using Orders.Actions.CreateOrderActions.CreateOrderDeleteAddress;
using Orders.Actions.CreateOrderActions.CreateOrderGetAddresses;
using Orders.Actions.CreateOrderActions.CreateOrderGetCountries;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("create-order")]
public class CreateOrderController : ControllerBase
{
    private readonly CreateOrderGetCountriesAction _createOrderGetCountriesAction;
    private readonly CreateOrderAddAddressAction _createOrderAddAddressesAction;
    private readonly CreateOrderGetAddressesAction _createOrderGetAddressesAction;
    private readonly CreateOrderDeleteAddressAction _createOrderDeleteAddressAction;
    private readonly CreateOrderCreateOrderAction _createOrderCreateOrderAction;
    private readonly GetProductsAction _getProductsAction;

    public CreateOrderController(
        CreateOrderGetCountriesAction createOrderGetCountriesAction,
        CreateOrderAddAddressAction createOrderAddAddressesAction,
        CreateOrderGetAddressesAction createOrderGetAddressesAction,
        CreateOrderDeleteAddressAction createOrderDeleteAddressAction,
        CreateOrderCreateOrderAction createOrderCreateOrderAction,
        GetProductsAction getProductsAction)
    {
        _createOrderGetCountriesAction = createOrderGetCountriesAction;
        _createOrderAddAddressesAction = createOrderAddAddressesAction;
        _createOrderGetAddressesAction = createOrderGetAddressesAction;
        _createOrderDeleteAddressAction = createOrderDeleteAddressAction;
        _createOrderCreateOrderAction = createOrderCreateOrderAction;
        _getProductsAction = getProductsAction;

    }

    [HttpGet("countries")]
    public async Task<ActionResult> GetCountries() => await _createOrderGetCountriesAction.Execute();

    [HttpGet("addresses")]
    public async Task<ActionResult> GetAddresses() => await _createOrderGetAddressesAction.Execute();

    [HttpGet("products")]
    public async Task<ActionResult> GetProducts() => await _getProductsAction.Execute();

    [HttpPost("addresses")]
    public async Task<ActionResult> AddAddress([FromBody] CreateOrderAddAddressPayload payload) => await _createOrderAddAddressesAction.Execute(payload);

    [HttpDelete("addresses/{id}")]
    public async Task<ActionResult> DeleteAddress(string id) => await _createOrderDeleteAddressAction.Execute(id);

    [HttpPost()]
    public async Task<ActionResult> CreateOrder([FromBody] CreateOrderCreateOrderPayload payload) => await _createOrderCreateOrderAction.Execute(payload);
}
