namespace WebApi.Controllers.Order
{
    public class AvailableDeliveriesGetListResponse
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
}
