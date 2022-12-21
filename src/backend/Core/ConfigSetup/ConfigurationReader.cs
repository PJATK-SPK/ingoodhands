using Core.Configuration.App;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Core.ConfigSetup
{
    public static class ConfigurationReader
    {
        private static AppConfig? _cache;

        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static AppConfig Get()
        {
            if (_cache != null) return _cache;

            var fileName = "config.json";
            var config = GetString(fileName);

            if (string.IsNullOrWhiteSpace(config))
                throw new ArgumentException($"Config not found! {fileName}");

            var result = JsonSerializer.Deserialize<AppConfig>(config, _options)!;
            FixConnectionStringByEnvVariables(result);
            _cache = result;

            return result;
        }

        private static void FixConnectionStringByEnvVariables(AppConfig result)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");

            if (dbHost != null)
            {
                var sb = new StringBuilder();
                sb.Append($"User ID={Environment.GetEnvironmentVariable("DB_USER")};");
                sb.Append($"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};");
                sb.Append($"Database={Environment.GetEnvironmentVariable("DB_DATABASE")};");
                sb.Append($"Host={dbHost};");
                sb.Append($"Port={Environment.GetEnvironmentVariable("DB_PORT")};");
                sb.Append($"Connection Lifetime=0;");
                result.ConnectionStrings.Database = sb.ToString();
            }
        }

        public static string GetString(string fileName)
        {
            string path = GetConfigFullPath(fileName);
            var result = File.ReadAllText(path);
            return result;
        }

        public static string GetConfigFullPath(string fileName)
        {
            var env = GetEnvironment();
            var location = GetCurrentLocation();
            var result =
                Path.Combine(
                    Path.GetDirectoryName(location)!,
                    Path.Join("Configuration/", env.ToString(), fileName)
                );
            return result;
        }

        public static ConfigurationEnvironment GetEnvironment()
        {
            var location = GetCurrentLocation();
            var path =
                Path.Combine(
                    Path.GetDirectoryName(location)!,
                    Path.Join("Configuration/environment.json")
                );
            var json = File.ReadAllText(path);
            var jsonObject = JsonSerializer.Deserialize<ConfigurationEnvironmentJson>(json, _options)!;
            var result = Enum.Parse<ConfigurationEnvironment>(jsonObject.Name);
            return result;
        }

        private static string GetCurrentLocation()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var result = new Uri(location).LocalPath;
            return result.ToString();
        }

        internal class ConfigurationEnvironmentJson { public string Name { get; set; } = "Local"; }
    }
}
