using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesCount;
using System.Linq.Dynamic.Core;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("available-deliveries")]
public class AvailableDeliveriesController : ControllerBase
{
    private readonly AvailableDeliveriesCountAction _availableDeliveriesCountAction;

    public AvailableDeliveriesController(AvailableDeliveriesCountAction availableDeliveriesCountAction)
    {
        _availableDeliveriesCountAction = availableDeliveriesCountAction;
    }

    // Sandro: Analogia do MyDonationsController::GetCountOfNotDelivered. Sprawdzamy czy pan jest dostawc¹ i bierymy jego warehouse
    // Nastepnie sprawdzamy ile jest deliverek do wziêcia dla tego warehouseu (czyli TripStarted = 0 && IsLost = 0) i liczymy
    // UWAGAA !!!!!!!!!! GDY PAN NIE MA WAREHOUSEID ZWRACAMY 0 (ZERO)
    [HttpGet("count")]
    public async Task<ActionResult> GetWarehouseDeliveriesCount() => await _availableDeliveriesCountAction.Execute();

    // Sandro: Spradzamy czy pan dostawca ma ju¿ wziête na pok³ad jakieœ delivery (TripStarted=1 && DelivererId == CurrentUser.Id && IsDelivered = false)
    // IsDelivered jest kluczowe, bo przeciez móg³ mieæ jakies w przeszlosci a nas itneresuje czy TERAZ ma jakies aktywne.
    // Jeœli ma no to true, jeœli nie ma to false.
    public class DeleteMe2 { public bool Result { get; set; } = true; }
    [HttpGet("has-active-delivery")]
    public async Task<ActionResult> HasActiveDelivery() => await Task.Run(() => Ok(new DeleteMe2()));

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
