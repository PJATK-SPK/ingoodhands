namespace AuthService.Actions.AuthActions.PostLogin
{
    public class PostLoginActionResponse
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
    }
}
