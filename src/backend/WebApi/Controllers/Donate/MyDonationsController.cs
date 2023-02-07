using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Donate;

[EnableCors]
[ApiController]
[Authorize]
[Route("my-donations")]
public class MyDonationsController : ControllerBase
{
    public class DeleteMeMyDonationsResponseItem
    {
        public string Name { get; set; } = default!; // DNT...
        public int ProductsCount { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public bool IsDelivered { get; set; }
    }
    [HttpGet]
    public async Task<ActionResult> GetList()
        => await Task.FromResult(Ok(new List<DeleteMeMyDonationsResponseItem>
        {
            new DeleteMeMyDonationsResponseItem
            {
                Name = "DNT000123",
                ProductsCount = 10,
                CreationDate = DateTime.Now,
                IsDelivered = false,
            }
        }));

    [HttpGet("not-delivered-count")]
    public async Task<ActionResult> GetCountOfNotDelivered()
        => await Task.FromResult(Ok(new { Count = 3 }));

    public class DeleteMeMyDonationDetailsProductResponse
    {
        public string Name { get; set; } = default!; // Apple
        public int Quantity { get; set; }
        public string Unit { get; set; } = default!;
    }
    public class DeleteMeMyDonationDetailsWarehouseResponse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string CountryName { get; set; } = default!;
        public double GpsLatitude { get; set; }
        public double GpsLongitude { get; set; }
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string Street { get; set; } = default!;
    }
    public class DeleteMeMyDonationDetailsResponse
    {
        public string Name { get; set; } = default!; // DNT...
        public DateTime CreationDate { get; set; } = default!;
        public bool IsDelivered { get; set; }
        public bool IsExpired { get; set; }
        public DeleteMeMyDonationDetailsWarehouseResponse Warehouse { get; set; } = default!;
        public List<DeleteMeMyDonationDetailsProductResponse> Products { get; set; } = default!;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetDetails(string id)
        => await Task.FromResult(Ok(new DeleteMeMyDonationDetailsResponse
        {
            Name = "DNT000123",
            Warehouse = new DeleteMeMyDonationDetailsWarehouseResponse
            {
                CountryName = "Poland",
                Id = "asj65n87",
                Name = "PL001",
                GpsLatitude = 52.403324,
                GpsLongitude = 16.917781,
                City = "Kwidzyn",
                Street = "Jaworowa 3/5",
                PostalCode = "82-500"
            },
            Products = new List<DeleteMeMyDonationDetailsProductResponse>
            {
                 new DeleteMeMyDonationDetailsProductResponse
                 {
                      Name = "Apple",
                      Quantity = 1,
                      Unit = "kg",
                 }
            }
        }));
}
