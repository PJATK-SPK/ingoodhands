namespace AuthService.Actions.UserSettingsActions.PatchUserDetails
{
    public class PatchUserDetailsPayload
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
