using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[EnableCors]
[ApiController]
[Route("donate-form")]
public class DonateFormController : ControllerBase
{
    internal class DeleteMeGetWarehousesResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string CountryName { get; set; } = default!;
        public double GpsLatitude { get; set; }
        public double GpsLongitude { get; set; }
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string Street { get; set; } = default!;
    }
    [HttpGet("warehouses")]
    public async Task<ActionResult> GetWarehouses()
    {
        return await Task.FromResult(Ok(new List<DeleteMeGetWarehousesResponse>
        {
            new DeleteMeGetWarehousesResponse
            {
                CountryName = "Poland",
                Id=1,
                Name="PL001",
                GpsLatitude=52.403324,
                GpsLongitude=16.917781,
                City="Kwidzyn",
                Street="Jaworowa 3/5",
                PostalCode="82-500"
            },
             new DeleteMeGetWarehousesResponse
            {
                CountryName = "France",
                Id=2,
                Name="FR001",
                GpsLatitude=12.403324,
                GpsLongitude=26.917781,
                City="Paris",
                Street="Bleble 21",
                PostalCode="123-321"
            }
        }));
    }

    internal class DeleteMeGetProductsResponse
    {
        public long Id { get; set; } // ProductId
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
               Id = 1,
               Name = "Fruits",
               Unit = "kg"
            },
            new DeleteMeGetProductsResponse
            {
               Id = 2,
               Name = "Water",
               Unit = "l"
            },
            new DeleteMeGetProductsResponse
            {
               Id = 3,
               Name = "Rice",
               Unit = "kg"
            },
            new DeleteMeGetProductsResponse
            {
               Id = 4,
               Name = "Milk",
               Unit = "l"
            },
            new DeleteMeGetProductsResponse
            {
               Id = 5,
               Name = "Meat",
               Unit = "kg"
            }
        }));
    }

    public class DeleteMePerformDonatePayloadProduct
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
    }
    public class DeleteMePerformDonatePayload
    {
        public long WarehouseId { get; set; }
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
}
