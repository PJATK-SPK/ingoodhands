using Core.Database.Seeders;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("request-help")]
public class RequestHelpController : ControllerBase
{
    internal class DeleteMeGetMapItemBaseResponse
    {
        public string Name { get; set; } = default!;
        public double GpsLatitude { get; set; }
        public double GpsLongitude { get; set; }
    }

    internal class DeleteMeGetMapWarehouseItemResponse : DeleteMeGetMapItemBaseResponse { }
    internal class DeleteMeGetMapOrderItemResponse : DeleteMeGetMapItemBaseResponse { }

    internal class DeleteMeGetMapResponse
    {
        public List<DeleteMeGetMapWarehouseItemResponse> Warehouses { get; set; } = default!;
        public List<DeleteMeGetMapOrderItemResponse> Orders { get; set; } = default!;
    }
    [HttpGet("map")]
    public async Task<ActionResult> GetMap()
    {
        var appDbContextProducts = new DeleteMeGetMapResponse
        {
            Warehouses = new List<DeleteMeGetMapWarehouseItemResponse>
            {
                 new DeleteMeGetMapWarehouseItemResponse
                 {
                     Name="PL001",
                     GpsLatitude= AddressSeeder.Address1Poland.GpsLatitude, // should be fetched from db, not from seeder
                     GpsLongitude= AddressSeeder.Address1Poland.GpsLongitude,
                 },
                 new DeleteMeGetMapWarehouseItemResponse
                 {
                     Name="PL002",
                     GpsLatitude= AddressSeeder.Address2Poland.GpsLatitude, // should be fetched from db, not from seeder
                     GpsLongitude= AddressSeeder.Address2Poland.GpsLongitude,
                 },
            },
            Orders = new List<DeleteMeGetMapOrderItemResponse>
            {
                new DeleteMeGetMapOrderItemResponse
                {
                    Name = "ORD000001",
                    GpsLatitude = AddressSeeder.Address6Hungary.GpsLatitude,
                    GpsLongitude = AddressSeeder.Address6Hungary.GpsLongitude,
                },
                new DeleteMeGetMapOrderItemResponse
                {
                    Name = "ORD000002",
                    GpsLatitude = AddressSeeder.Address7Czech.GpsLatitude,
                    GpsLongitude = AddressSeeder.Address7Czech.GpsLongitude,
                }
            }
        };

        return await Task.FromResult(Ok(appDbContextProducts));
    }
}
