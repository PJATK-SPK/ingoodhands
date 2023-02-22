namespace Core.Setup.ConfigSetup
{
    public class AppConfig
    {
        public string OAuth2Authority { get; set; } = default!;
        public string OAuth2Audience { get; set; } = default!;
        public string OAuth2ClientId { get; set; } = default!;
        public string OAuth2Scopes { get; set; } = default!;
        public string DatabaseConnectionString { get; set; } = default!;
        public string FrontendURL { get; set; } = default!;
        public string HashidsSalt { get; set; } = default!;
    }
}
