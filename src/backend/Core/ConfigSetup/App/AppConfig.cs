using Core.ConfigSetup;

namespace Core.Configuration.App
{
    public class AppConfig
    {
        public readonly ConfigurationEnvironment Environment = ConfigurationReader.GetEnvironment();
        public AppConfigAuthorization Authorization { get; set; } = new AppConfigAuthorization();
        public AppConfigConnectionStrings ConnectionStrings { get; set; } = new AppConfigConnectionStrings();
        public AppConfigUrls Urls { get; set; } = new AppConfigUrls();
        public string HashidsSalt { get; set; } = default!;
    }
}
