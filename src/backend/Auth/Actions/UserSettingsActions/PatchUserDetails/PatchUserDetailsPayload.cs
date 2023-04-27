namespace Auth.Actions.UserSettingsActions.PatchUserDetails
{
    public class PatchUserDetailsPayload
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
