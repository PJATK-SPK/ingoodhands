namespace Orders.Actions.CreateOrderActions.Shared
{
    public interface IAddressDto
    {
        string Id { get; set; }
        string CountryName { get; set; }
        string PostalCode { get; set; }
        string City { get; set; }
        string? Street { get; set; }
        string? StreetNumber { get; set; }
        string? Apartment { get; set; }
        double GpsLatitude { get; set; }
        double GpsLongitude { get; set; }
    }
}