namespace Orders.Actions.OrdersActions.OrdersGetSingle
{
    public class OrdersGetSingleDeliveryResponse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public bool IsDelivered { get; set; } = default!;
    }
}
