using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    internal class DeleteMeMyOrdersResponseItem
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public int Percentage { get; set; } = default!;
    }
    [HttpGet]
    public async Task<ActionResult> GetList() // zwracamy wszystkie ordery usera
    {
        var appDbContextProducts = new List<DeleteMeMyOrdersResponseItem>
        {
           new DeleteMeMyOrdersResponseItem
           {
               Id="b654wv",
               Name = "ORD000001",
               Percentage = 25,
               CreationDate = DateTime.UtcNow.AddDays(-5),
           }
        };

        return await Task.FromResult(Ok(appDbContextProducts));
    }

    internal class DeleteMeOrderDeliveryResponse
    {
        public string Name { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public bool IsDelivered { get; set; } = default!;
    }
    internal class DeleteMeOrderProductResponse
    {
        public string Name { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Unit { get; set; } = default!;
    }
    internal class DeleteMeOrderResponseItem
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Percentage { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public string CountryName { get; set; } = default!;
        public double GpsLatitude { get; set; }
        public double GpsLongitude { get; set; }
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string Street { get; set; } = default!;
        public List<DeleteMeOrderDeliveryResponse> Deliveries { get; set; } = default!;
        public List<DeleteMeOrderProductResponse> Products { get; set; } = default!;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(string id)
    {
        var appDbContextProducts = new List<DeleteMeOrderResponseItem>
        {
           new DeleteMeOrderResponseItem
           {
               Id = "knb34",
               Name = "ORD000001",
               Percentage = 25,
               CreationDate = DateTime.UtcNow.AddDays(-5),
               CountryName = "Ukraine",
               GpsLatitude = 12.23,
               GpsLongitude = 23.34,
               City="Kyiv",
               PostalCode="00-000",
               Street = "Gdañska 1",
               Deliveries = new List<DeleteMeOrderDeliveryResponse>
               {
                    new DeleteMeOrderDeliveryResponse
                    {
                         Name = "DEL000002",
                         CreationDate= DateTime.UtcNow.AddDays(-5),
                         IsDelivered=false,
                    },
                    new DeleteMeOrderDeliveryResponse
                    {
                         Name = "DEL000001",
                         CreationDate= DateTime.UtcNow.AddDays(-10),
                         IsDelivered=true,
                    }
               },
               Products= new List<DeleteMeOrderProductResponse>
               {
                   new DeleteMeOrderProductResponse
                   {
                        Name="Milk",
                        Quantity=10,
                        Unit = "l"
                   },
                   new DeleteMeOrderProductResponse
                   {
                        Name="Rice",
                        Quantity=15,
                        Unit = "kg"
                   }
               }
           }
        };

        return await Task.FromResult(Ok(appDbContextProducts));
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> Cancel(string id)
    {
        return await Task.FromResult(Ok(new { Message = "OK" }));
    }
}
