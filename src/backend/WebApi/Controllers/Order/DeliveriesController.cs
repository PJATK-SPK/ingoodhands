using Core.Actions.WarehouseName.GetWarehouseName;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.DeliveriesActions.DliveriesGetList;
using System.Linq.Dynamic.Core;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("deliveries")]
public class DeliveriesController : ControllerBase
{
    private readonly GetWarehouseNameAction _getWarehouseNameAction;
    private readonly DliveriesGetListAction _dliveriesGetListAction;

    public DeliveriesController(
        GetWarehouseNameAction getWarehouseNameAction,
        DliveriesGetListAction dliveriesGetListAction)
    {
        _getWarehouseNameAction = getWarehouseNameAction;
        _dliveriesGetListAction = dliveriesGetListAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize, string? filter) => await _dliveriesGetListAction.Execute(page, pageSize, filter);

    public class DeleteMeResponse2Product
    {
        public string Name { get; set; } = default!;
        public string Unit { get; set; } = default!;
        public int Quantity { get; set; }
    }
    public class DeleteMeResponse2
    {
        public string Id { get; set; } = default!;
        public string DeliveryName { get; set; } = default!;
        public string OrderName { get; set; } = default!;
        public bool IsDelivered { get; set; } = default!;
        public bool IsLost { get; set; } = default!;
        public bool TripStarted { get; set; } = default!;
        public string DelivererFullName { get; set; } = default!;
        public string CountryName { get; set; } = default!;
        public double GpsLatitude { get; set; }
        public double GpsLongitude { get; set; }
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string FullStreet { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public List<DeleteMeResponse2Product> Products { get; set; } = default!;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(string id)
    {
        var result = new DeleteMeResponse2
        {
            Id = "brtysa3",
            DeliveryName = "DEL000001",
            OrderName = "ORD000001",
            IsDelivered = false,
            IsLost = false,
            TripStarted = false,
            CountryName = "Poland",
            GpsLatitude = 12.23,
            GpsLongitude = 23.24,
            City = "Poznañ",
            PostalCode = "12-234",
            FullStreet = "Poznanska 12/3",
            DelivererFullName = "YourStory Pijony",
            CreationDate = DateTime.UtcNow,
            Products = new List<DeleteMeResponse2Product>
            {
                new DeleteMeResponse2Product
                {
                    Name = "Milk",
                    Unit = "l", // PABLO PAMIETAJ LOWERCASE()!!!
                    Quantity = 20,
                },
                new DeleteMeResponse2Product
                {
                    Name = "Cereals",
                    Unit = "kg", // PABLO PAMIETAJ LOWERCASE()!!!

                    Quantity = 100,
                }
            }
        };

        return await Task.FromResult(Ok(result));
    }

    [HttpPost("{id}/pickup")]
    public async Task<ActionResult> Pickup(string id)
    {
        // ustawiasz tripstarted =1
        return await Task.FromResult(Ok(new { Message = "OK" }));
    }

    [HttpGet("warehouse-name")]
    public async Task<ActionResult> GetWarehouseName() => await _getWarehouseNameAction.Execute();

    [HttpPost("{id}/set-lost")]
    public async Task<ActionResult> SetLost(string id)
    {
        // ustawiasz islost =1
        return await Task.FromResult(Ok(new { Message = "OK" }));
    }
}
