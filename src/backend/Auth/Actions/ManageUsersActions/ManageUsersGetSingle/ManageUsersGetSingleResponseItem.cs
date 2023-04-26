namespace Auth.Actions.ManageUsersActions.ManageUsersGetSingle
{
    public class ManageUsersGetSingleResponseItem
    {
        public string Id { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PictureUrl { get; set; } = default!;
        public string? WarehouseId { get; set; }
        public List<string> Roles { get; set; } = default!;
    }
}
