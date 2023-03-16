namespace Orders.Actions.OrdersActions.OrdersGetSingle
{
    public class OrdersGetSingleResponse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Percentage { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public string CountryName { get; set; } = default!;
        public double GpsLatitude { get; set; }
        public double GpsLongitude { get; set; }
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string FullStreet { get; set; } = default!;
        public List<OrdersGetSingleDeliveryResponse> Deliveries { get; set; } = default!;
        public List<OrdersGetSingleProductResponse> Products { get; set; } = default!;
    }
}
