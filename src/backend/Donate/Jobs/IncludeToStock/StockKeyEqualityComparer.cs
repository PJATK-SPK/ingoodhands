namespace Donate.Jobs.IncludeToStock
{
    public class StockKeyEqualityComparer : IEqualityComparer<(long ProductId, long WarehouseId)>
    {
        public bool Equals((long ProductId, long WarehouseId) x, (long ProductId, long WarehouseId) y)
        {
            return x.ProductId == y.ProductId && x.WarehouseId == y.WarehouseId;
        }

        public int GetHashCode((long ProductId, long WarehouseId) obj)
        {
            int hash = 17;
            hash = hash * 31 + obj.ProductId.GetHashCode();
            hash = hash * 31 + obj.WarehouseId.GetHashCode();
            return hash;
        }
    }
}