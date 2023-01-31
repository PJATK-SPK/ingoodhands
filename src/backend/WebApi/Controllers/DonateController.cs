using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[EnableCors]
[ApiController]
[Route("donate")]
public class DonateController : ControllerBase
{

    internal class DeleteMeGetWarehousesDto
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
        return await Task.FromResult(Ok(new List<DeleteMeGetWarehousesDto>
        {
            new DeleteMeGetWarehousesDto
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
             new DeleteMeGetWarehousesDto
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

    internal class DeleteMeGetProductsDto
    {
        public string Name { get; set; } = default!;
        public string Unit { get; set; } = default!;
    }
    [HttpGet("products")]
    public async Task<ActionResult> GetProducts()
    {
        return await Task.FromResult(Ok(new List<DeleteMeGetProductsDto>
        {
            new DeleteMeGetProductsDto
            {
               Name = "Fruits",
               Unit = "kg"
            },
            new DeleteMeGetProductsDto
            {
               Name = "Water",
               Unit = "l"
            },
            new DeleteMeGetProductsDto
            {
               Name = "Rice",
               Unit = "kg"
            },
            new DeleteMeGetProductsDto
            {
               Name = "Milk",
               Unit = "l"
            },
            new DeleteMeGetProductsDto
            {
               Name = "Meat",
               Unit = "kg"
            }
        }));
    }
}
