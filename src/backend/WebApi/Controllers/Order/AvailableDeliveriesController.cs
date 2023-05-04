using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesCount;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesHasActiveDelivery;
using System.Linq.Dynamic.Core;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("available-deliveries")]
public class AvailableDeliveriesController : ControllerBase
{
    private readonly AvailableDeliveriesCountAction _availableDeliveriesCountAction;
    private readonly AvailableDeliveriesHasActiveDeliveryAction _availableDeliveriesHasActiveDeliveryAction;

    public AvailableDeliveriesController(AvailableDeliveriesCountAction availableDeliveriesCountAction, AvailableDeliveriesHasActiveDeliveryAction availableDeliveriesHasActiveDeliveryAction)
    {
        _availableDeliveriesCountAction = availableDeliveriesCountAction;
        _availableDeliveriesHasActiveDeliveryAction = availableDeliveriesHasActiveDeliveryAction;
    }

    [HttpGet("count")]
    public async Task<ActionResult> GetWarehouseDeliveriesCount() => await _availableDeliveriesCountAction.Execute();

    [HttpGet("has-active-delivery")]
    public async Task<ActionResult> HasActiveDelivery() => await _availableDeliveriesHasActiveDeliveryAction.Execute();

    // Sandro: Sprawdzamy czy to pan dostawca i przypisujemy danej deliverce tego pana
    // dodatkowo ustawiamy TripStarted = 1
    [HttpPost("assign-delivery/{deliveryId}")]
    public async Task<ActionResult> AssignDelivery(string deliveryId) => await Task.Run(() => Ok());

    // Sandro: Bierymy dostepne deliverki (czyli TripStarted = 0 && IsLost = 0) z warehouseu danego pana dostawcy.
    // no i wiadomo obsluga searcha i paginacji
    public class DeleteMe4
    {
        public string Id { get; set; } = default!;
        public string DeliveryName { get; set; } = default!;
        public string OrderName { get; set; } = default!;
        public string WarehouseCountryEnglishName { get; set; } = default!;
        public string WarehouseName { get; set; } = default!;
        public string WarehouseFullStreet { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public int ProductTypesCount { get; set; } = default!;
    }
    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize, string? filter)
    {
        var arr = new DeleteMe4[]{
            new DeleteMe4
            {
                 Id="bt53",
                 DeliveryName = "DNT0001",
                 OrderName="ORD0001",
                 WarehouseCountryEnglishName = "Poland",
                 WarehouseName = "PL01",
                 WarehouseFullStreet = "Furmañska 13/3",
                 CreationDate = DateTime.Now,
                 ProductTypesCount=1,
            }
        };
        var res = new PagedResult<DeleteMe4>()
        {
            CurrentPage = 1,
            PageCount = 1,
            PageSize = 1,
            Queryable = arr.AsQueryable(),
            RowCount = 0
        };

        return Ok(res);
    }
}
