using Orders.Actions.CreateOrderActions.Shared;

namespace Orders.Actions.CreateOrderActions.CreateOrderAddAddress
{
    public class CreateOrderAddAddressResponse : IAddressDto
    {
        public string Id { get; set; } = default!;
        public string CountryName { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string City { get; set; } = default!;
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? Apartment { get; set; }
        public double GpsLatitude { get; set; } = default!;
        public double GpsLongitude { get; set; } = default!;
    }
}
