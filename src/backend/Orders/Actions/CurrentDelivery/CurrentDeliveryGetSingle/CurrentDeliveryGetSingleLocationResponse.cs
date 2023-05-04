namespace Orders.Actions.CurrentDelivery.CurrentDeliveryGetSingle
{
    public class CurrentDeliveryGetSingleLocationResponse
    {
        public string CountryName { get; set; } = default!;
        public double GpsLatitude { get; set; }
        public double GpsLongitude { get; set; }
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string FullStreet { get; set; } = default!;
    }
}
