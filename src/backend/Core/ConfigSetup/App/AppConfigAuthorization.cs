namespace Core.Configuration.App
{
    public class AppConfigAuthorization
    {
        public string Authority { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public string Scopes { get; set; } = default!;
    }
}
