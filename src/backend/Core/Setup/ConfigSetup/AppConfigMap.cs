namespace Core.Setup.ConfigSetup
{
    public static class AppConfigMap
    {
        public static readonly IReadOnlyDictionary<string, string> AppToEnvNames = new Dictionary<string, string>
        {
            { nameof(AppConfig.OAuth2Authority), "OAUTH2_AUTHORITY" },
            { nameof(AppConfig.OAuth2Audience), "OAUTH2_AUDIENCE" },
            { nameof(AppConfig.OAuth2ClientId), "OAUTH2_CLIENT_ID" },
            { nameof(AppConfig.OAuth2Scopes), "OAUTH2_SCOPES" },
            { nameof(AppConfig.FrontendURL), "FRONTEND_URL" },
            { nameof(AppConfig.HashidsSalt), "HASHIDS_SALT" },
        };

        public static readonly IReadOnlyDictionary<string, string> DbToEnvNames = new Dictionary<string, string>
        {
            { "User ID", "DB_USER" },
            { "Host", "DB_HOST" },
            { "Password", "DB_PASSWORD" },
            { "Database", "DB_DATABASE" },
            { "Port", "DB_PORT" },
        };
    }
}
