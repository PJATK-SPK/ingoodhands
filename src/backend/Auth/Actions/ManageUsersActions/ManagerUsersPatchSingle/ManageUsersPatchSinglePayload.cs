namespace Auth.Actions.ManageUsersActions.ManagerUsersPatchSingle
{
    public class ManageUsersPatchSinglePayload
    {
        public string? WarehouseId { get; set; }
        public List<string> Roles { get; set; } = default!;
    }
}
