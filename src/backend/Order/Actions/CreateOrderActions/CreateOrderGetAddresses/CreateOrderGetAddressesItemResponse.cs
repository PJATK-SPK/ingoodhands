namespace Orders.Actions.CreateOrderActions.CreateOrderGetAddresses
{
    public class CreateOrderGetAddressesItemResponse
    {
        public string Id { get; set; } = default!;
        public string CountryName { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string City { get; set; } = default!;
        public string? FullStreet { get; set; }
    }
}
