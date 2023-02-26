namespace Donate.Actions.MyDonations.GetDetails
{
    public class GetMyDonationDetailsResponse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!; // DNT...
        public DateTime CreationDate { get; set; } = default!;
        public DateTime ExpireDate { get; set; } = default!;
        public bool IsDelivered { get; set; }
        public bool IsExpired { get; set; }
        public GetMyDonationDetailsWarehouseResponse Warehouse { get; set; } = default!;
        public List<GetMyDonationDetailsProductResponse> Products { get; set; } = default!;
    }
}
