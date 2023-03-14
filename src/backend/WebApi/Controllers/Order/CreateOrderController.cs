using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddresses;
using Orders.Actions.CreateOrderActions.CreateOrderGetCountries;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("create-order")]
public class CreateOrderController : ControllerBase
{
    private readonly CreateOrderGetCountriesAction _createOrderGetCountriesAction;
    private readonly CreateOrderAddAddressAction _createOrderAddAddressesAction;

    public CreateOrderController(
        CreateOrderGetCountriesAction createOrderGetCountriesAction,
        CreateOrderAddAddressAction createOrderAddAddressesAction
        )
    {
        _createOrderGetCountriesAction = createOrderGetCountriesAction;
        _createOrderAddAddressesAction = createOrderAddAddressesAction;
    }

    [HttpGet("countries")]
    public async Task<ActionResult> GetCountries() => await _createOrderGetCountriesAction.Execute();

    public class DeleteMeAddressDto
    {
        public string Id { get; set; } = default!;
        public string CountryName { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string City { get; set; } = default!;
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? Apartment { get; set; }
        public double GpsLatitude { get; set; } = default!;
        public double GpsLongitude { get; set; } = default!;
    }
    public class DeleteMeGetAddressesItem : DeleteMeAddressDto { }
    public class DeleteMeAddAddressPayload : DeleteMeAddressDto { }
    public class DeleteMeAddAddressResponse : DeleteMeAddressDto { }

    [HttpGet("addresses")]
    public async Task<ActionResult> GetAddresses() // wszystkie active 
    {
        var appDbContextAddresses = new List<DeleteMeGetAddressesItem>
        {
           new DeleteMeGetAddressesItem
           {
                Id = "b654wv",
                CountryName = "Ukraine",
                PostalCode = "82-420",
                City = "Bachmut",
                Street = "Papieska",
                StreetNumber= "21",
                Apartment= "37",
                GpsLatitude=1.111,
                GpsLongitude=2.222
           }
        };

        return await Task.FromResult(Ok(appDbContextAddresses));
    }

    [HttpPost("addresses")]
    public async Task<ActionResult> AddAddress([FromBody] CreateOrderAddAddressPayload payload) => await _createOrderAddAddressesAction.Execute(payload);


    [HttpDelete("addresses/{id}")]
    public async Task<ActionResult> DeleteAddress(string id)
    {
        // address.status = inactive
        // useraddress.isdeletedbyuser = true
        // useraddress.status = inactive
        var deletedAddress = new List<DeleteMeAddAddressResponse>
        {
           new DeleteMeAddAddressResponse
           {
                Id = "b654wv",
                CountryName = "Ukraine",
                PostalCode = "82-420",
                City = "Bachmut",
                Street = "Papieska",
                StreetNumber= "21",
                Apartment= "37",
                GpsLatitude=1.111,
                GpsLongitude=2.222
           }
        };

        return await Task.FromResult(Ok(deletedAddress));
    }

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
