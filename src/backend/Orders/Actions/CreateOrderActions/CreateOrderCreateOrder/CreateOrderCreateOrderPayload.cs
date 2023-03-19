namespace Orders.Actions.CreateOrderActions.CreateOrderCreateOrder
{
    public class CreateOrderCreateOrderPayload
    {
        public string AddressId { get; set; } = default!;
        public List<CreateOrderCreateOrderProductPayload> Products { get; set; } = default!;
    }
}
