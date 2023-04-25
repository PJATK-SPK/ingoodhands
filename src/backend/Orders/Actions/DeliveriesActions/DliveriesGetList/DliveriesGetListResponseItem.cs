namespace Orders.Actions.DeliveriesActions.DliveriesGetList
{
    public class DliveriesGetListResponseItem
    {
        public string Id { get; set; } = default!;
        public string DeliveryName { get; set; } = default!;
        public string OrderName { get; set; } = default!;
        public bool IsDelivered { get; set; } = default!;
        public bool IsLost { get; set; } = default!;
        public bool TripStarted { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public int ProductTypesCount { get; set; } = default!;
    }
}
