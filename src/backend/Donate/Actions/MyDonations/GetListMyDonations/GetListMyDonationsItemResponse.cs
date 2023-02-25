namespace Donate.Actions.MyDonations.GetList
{
    public class GetListMyDonationsItemResponse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!; // DNT...
        public int ProductsCount { get; set; } = default!;
        public DateTime CreationDate { get; set; } = default!;
        public bool IsDelivered { get; set; }
        public bool IsExpired { get; set; }
    }
}
