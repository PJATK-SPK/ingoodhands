using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddress;
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

    public CreateOrderController(
        CreateOrderGetCountriesAction createOrderGetCountriesAction,
        CreateOrderAddAddressAction createOrderAddAddressesAction,
        CreateOrderGetAddressesAction createOrderGetAddressesAction,
        CreateOrderDeleteAddressAction createOrderDeleteAddressAction)
    {
        _createOrderGetCountriesAction = createOrderGetCountriesAction;
        _createOrderAddAddressesAction = createOrderAddAddressesAction;
        _createOrderGetAddressesAction = createOrderGetAddressesAction;
        _createOrderDeleteAddressAction = createOrderDeleteAddressAction;
    }

    [HttpGet("countries")]
    public async Task<ActionResult> GetCountries() => await _createOrderGetCountriesAction.Execute();

    [HttpGet("addresses")]
    public async Task<ActionResult> GetAddresses() => await _createOrderGetAddressesAction.Execute();

    [HttpPost("addresses")]
    public async Task<ActionResult> AddAddress([FromBody] CreateOrderAddAddressPayload payload) => await _createOrderAddAddressesAction.Execute(payload);

    [HttpDelete("addresses/{id}")]
    public async Task<ActionResult> DeleteAddress(string id) => await _createOrderDeleteAddressAction.Execute(id);

    public class DeleteMeCreateOrderProductPayload
    {
        public string Name { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Unit { get; set; } = default!;
    }
    public class DeleteMeCreateOrderPayload
    {
        public string AddressId { get; set; } = default!;
        public List<DeleteMeCreateOrderProductPayload> Products { get; set; } = default!;
    }

    [HttpPost()]
    public async Task<ActionResult> CreateOrder([FromBody] DeleteMeCreateOrderPayload payload) // wszystkie active 
    {
        // tutaj zwracamy obiekt identyczny jak w GET orders (GetSingle())
        return await Task.FromResult(Ok(new { Name = "ORD0001" }));
    }
}
