using Auth.Actions.ManageUsersActions.ManageUsersGetSingle;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Orders.Actions.OrdersActions.OrdersCancel;
using System.Drawing.Printing;
using System.Linq.Dynamic.Core;
using static WebApi.Controllers.Order.StocksController;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("deliveries")]
public class DeliveriesController : ControllerBase
{
    public class DeleteMeResponse1
    {
        public string Id { get; set; } = default!;
        public string DeliveryName { get; set; } = default!;
        public string OrderName { get; set; } = default!;
        public bool IsDelivered { get; set; } = default!;
        public bool IsLost { get; set; } = default!;
        public bool TripStarted { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public int ProductTypesCount { get; set; } = default!;
    }
    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize, string? filter)
    {
        // nie zapomnij o tych 3 parametrach xd
        var appDbContextProducts = new List<DeleteMeResponse1>
        {
            new DeleteMeResponse1
            {
                Id="b4653",
                DeliveryName = "DEL000001",
                OrderName = "ORD000001",
                IsDelivered=false,
                IsLost=false,
                TripStarted=false,
                CreationDate= DateTime.UtcNow,
                ProductTypesCount=3, // Because 100xMilk, 20xRice, 1xCereals -> 3 types
            },
            new DeleteMeResponse1
            {
                Id="b4623",
                DeliveryName = "DEL000002",
                OrderName = "ORD000002",
                IsDelivered=true,
                IsLost=true,
                TripStarted=true,
                CreationDate= DateTime.UtcNow,
                ProductTypesCount=1,
            }
        }.AsQueryable();

        var result = appDbContextProducts.PageResult(page, pageSize);

        return await Task.FromResult(Ok(result));
    }

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
    public async Task<ActionResult> GetWarehouseName() => await Task.Run(() => Ok(new DeleteMeResponse { WarehouseName = "PL099" }));

    [HttpPost("{id}/set-lost")]
    public async Task<ActionResult> SetLost(string id)
    {
        // ustawiasz islost =1
        return await Task.FromResult(Ok(new { Message = "OK" }));
    }
}
