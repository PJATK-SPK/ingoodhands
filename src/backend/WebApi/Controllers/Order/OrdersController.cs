using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.OrdersActions.OrdersGetSingle;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly OrdersGetSingleAction _ordersGetSingleAction;

    public OrdersController(OrdersGetSingleAction ordersGetSingleAction)
    {
        _ordersGetSingleAction = ordersGetSingleAction;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(string id) => await _ordersGetSingleAction.Execute(id);
    //{
    //    var appDbContextProducts = new List<DeleteMeOrderResponseItem>
    //    {
    //       new DeleteMeOrderResponseItem
    //       {
    //           Id = "knb34",
    //           Name = "ORD000001",
    //           Percentage = 25,
    //           CreationDate = DateTime.UtcNow.AddDays(-5),
    //           CountryName = "Ukraine",
    //           GpsLatitude = 12.23,
    //           GpsLongitude = 23.34,
    //           City="Kyiv",
    //           PostalCode="00-000",
    //           Street = "Gdañska 1",
    //           Deliveries = new List<DeleteMeOrderDeliveryResponse>
    //           {
    //                new DeleteMeOrderDeliveryResponse
    //                {
    //                     Name = "DEL000002",
    //                     CreationDate= DateTime.UtcNow.AddDays(-5),
    //                     IsDelivered=false,
    //                },
    //                new DeleteMeOrderDeliveryResponse
    //                {
    //                     Name = "DEL000001",
    //                     CreationDate= DateTime.UtcNow.AddDays(-10),
    //                     IsDelivered=true,
    //                }
    //           },
    //           Products= new List<DeleteMeOrderProductResponse>
    //           {
    //               new DeleteMeOrderProductResponse
    //               {
    //                    Name="Milk",
    //                    Quantity=10,
    //                    Unit = "l"
    //               },
    //               new DeleteMeOrderProductResponse
    //               {
    //                    Name="Rice",
    //                    Quantity=15,
    //                    Unit = "kg"
    //               }
    //           }
    //       }
    //    };

    //    return await Task.FromResult(Ok(appDbContextProducts));
    //}

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> Cancel(string id)
    {
        return await Task.FromResult(Ok(new { Message = "OK" }));
    }
}
