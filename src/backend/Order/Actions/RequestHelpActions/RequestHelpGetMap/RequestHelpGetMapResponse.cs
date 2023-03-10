namespace Orders.Actions.RequestHelpActions.RequestHelpGetMap
{
    public class RequestHelpGetMapResponse
    {
        public List<RequestHelpGetMapWarehouseItemResponse> Warehouses { get; set; } = default!;
        public List<RequestHelpGetMapOrderItemResponse> Orders { get; set; } = default!;
    }
}