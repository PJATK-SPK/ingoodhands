namespace Auth.Actions.ManageUsersActions.ManageUsersPatchSingle
{
    public class ManageUsersPatchSingleResponseItem
    {
        public string Id { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string? WarehouseId { get; set; }
        public List<string> Roles { get; set; } = default!;
    }
}
