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
        public string CountryName { get; set; } = default!;
        public long WarehouseId { get; set; }
        public string WarehouseName { get; set; } = default!;
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
                WarehouseId=1,
                WarehouseName="PL01",
                GpsLatitude=52.403324,
                GpsLongitude=16.917781,
                City="Kwidzyn",
                Street="Jaworowa 3/5",
                PostalCode="82-500"
            }
        }));
    }
}
