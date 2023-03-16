namespace Orders.Actions.OrdersActions.OrdersGetSingle
{
    public class OrdersGetSingleDeliveryResponse
    {
        public string Name { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public bool IsDelivered { get; set; } = default!;
    }
}
