namespace Auth.Actions.ManageUsersActions.GetList
{
    public class GetSingleResponseItem
    {
        public string Id { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string? WarehouseName { get; set; }
        public List<string> Roles { get; set; } = default!;
    }
}
