using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.CurrentDelivery.CurrentDeliveryGetSingle;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("current-delivery")]
public class CurrentDeliveryController : ControllerBase
{
    private readonly CurrentDeliveryGetSingleAction _currentDeliveryGetSingleAction;

    public CurrentDeliveryController(CurrentDeliveryGetSingleAction currentDeliveryGetSingleAction)
    {
        _currentDeliveryGetSingleAction = currentDeliveryGetSingleAction;
    }

    // Sandro: Sprawdzamy czy pan to dostawca i szukamy deliverki, w której jest wpisany i jest NOT DELIVERED!
    // Pamietaj, że mogą być np 3 deliverki, które on obsługiwał i trzeba wziąć właśnie tą NOT DELIVERED
    [HttpGet()]
    public async Task<ActionResult> GetSingle() => await _currentDeliveryGetSingleAction.Execute();
    //{
    //    var result = new DeleteMeResponse2
    //    {
    //        Id = "brtysa3",
    //        DeliveryName = "DEL000001",
    //        OrderName = "ORD000001",
    //        WarehouseName = "PL001",
    //        IsDelivered = false,
    //        IsLost = false,
    //        TripStarted = false,
    //        NeedyEmail = "ineedhelp@wp.pl",
    //        NeedyFullName = "Adam Kowalski",
    //        NeedyPhoneNumber = "+48111222333",
    //        WarehouseLocation = new DeleteMe3
    //        {
    //            CountryName = "Poland",
    //            GpsLatitude = 12.23,
    //            GpsLongitude = 23.24,
    //            City = "Poznañ",
    //            PostalCode = "12-234",
    //            FullStreet = "Poznanska 12/3",
    //        },
    //        OrderLocation = new DeleteMe3
    //        {
    //            CountryName = "Ukraine",
    //            GpsLatitude = 23.34,
    //            GpsLongitude = 34.45,
    //            City = "Kyiv",
    //            PostalCode = "33-333",
    //            FullStreet = "Ukraińska 12/3",
    //        },
    //        DelivererFullName = "YourStory Pijony",
    //        CreationDate = DateTime.UtcNow,
    //        Products = new List<DeleteMeResponse2Product>
    //        {
    //            new DeleteMeResponse2Product
    //            {
    //                Name = "Milk",
    //                Unit = "l", // PABLO PAMIETAJ LOWERCASE()!!!
    //                Quantity = 20,
    //            },
    //            new DeleteMeResponse2Product
    //            {
    //                Name = "Cereals",
    //                Unit = "kg", // PABLO PAMIETAJ LOWERCASE()!!!

    //                Quantity = 100,
    //            }
    //        }
    //    };

    //    return await Task.FromResult(Ok(result));
    //}
}
