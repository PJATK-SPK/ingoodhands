namespace Donate.Actions.DonateForm.PerformDonate
{
    public class PerformDonatePayload
    {
        public string WarehouseId { get; set; } = default!;
        public List<PerformDonateProductPayload> Products { get; set; } = default!;
    }
}
