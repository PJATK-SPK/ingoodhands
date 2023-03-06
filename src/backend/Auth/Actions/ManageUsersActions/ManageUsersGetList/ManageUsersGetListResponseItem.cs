namespace Auth.Actions.ManageUsersActions.ManageUsersGetList
{
    public class ManageUsersGetListResponseItem
    {
        public string Id { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? WarehouseId { get; set; }
        public List<string> Roles { get; set; } = default!;
    }
}
