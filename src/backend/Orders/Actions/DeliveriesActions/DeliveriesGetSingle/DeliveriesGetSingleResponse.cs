namespace Orders.Actions.DeliveriesActions.DeliveriesGetSingle
{
    public class DeliveriesGetSingleResponse
    {
        public string Id { get; set; } = default!;
        public string DeliveryName { get; set; } = default!;
        public string OrderName { get; set; } = default!;
        public bool IsDelivered { get; set; } = default!;
        public bool IsLost { get; set; } = default!;
        public bool TripStarted { get; set; } = default!;
        public string DelivererFullName { get; set; } = default!;
        public string CountryName { get; set; } = default!;
        public double GpsLatitude { get; set; }
        public double GpsLongitude { get; set; }
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string FullStreet { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public List<DeliveriesGetSingleProductResponse> Products { get; set; } = default!;
    }
}
