namespace Orders.Actions.StocksActions.StocksGetList
{
    public class StocksGetListItemResponse
    {
        public string ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Unit { get; set; } = default!;
        public string WarehouseId { get; set; } = default!;
    }
}
