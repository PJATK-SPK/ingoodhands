namespace AuthService.BusinessLogic.PatchUserDetails
{
    public class PatchUserDetailsResponse
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
    }
}
