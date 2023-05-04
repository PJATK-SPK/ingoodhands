namespace Orders.Actions.OrdersActions.OrdersGetSingle
{
    public class OrdersGetSingleDeliveryResponse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public bool IsDelivered { get; set; } = default!;
        public bool TripStarted { get; set; } = default!;
        public bool IsLost { get; set; } = default!;
        public string? DelivererFullName { get; set; }
        public string? DelivererEmail { get; set; }
        public string? DelivererPhoneNumber { get; set; }
        public List<OrdersGetSingleProductResponse> Products { get; set; } = default!;
    }
}
