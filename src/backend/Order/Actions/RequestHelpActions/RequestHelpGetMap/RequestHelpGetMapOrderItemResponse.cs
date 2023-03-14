namespace Orders.Actions.RequestHelpActions.RequestHelpGetMap
{
    public class RequestHelpGetMapOrderItemResponse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public double GpsLatitude { get; set; }
        public double GpsLongitude { get; set; }
    }
}