namespace Orders.Actions.CurrentDelivery.CurrentDeliveryGetSingle
{
    public class CurrentDeliveryGetSingleResponse
    {
        public string Id { get; set; } = default!;
        public string WarehouseName { get; set; } = default!;
        public string DeliveryName { get; set; } = default!;
        public string OrderName { get; set; } = default!;
        public bool IsDelivered { get; set; } = default!;
        public bool IsLost { get; set; } = default!;
        public bool TripStarted { get; set; } = default!;
        public string DelivererFullName { get; set; } = default!;
        public string NeedyFullName { get; set; } = default!;
        public string NeedyPhoneNumber { get; set; } = default!;
        public string NeedyEmail { get; set; } = default!;
        public CurrentDeliveryGetSingleLocationResponse WarehouseLocation { get; set; } = default!;
        public CurrentDeliveryGetSingleLocationResponse OrderLocation { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public List<CurrentDeliveryGetSingleProductResponse> Products { get; set; } = default!;
    }
}
