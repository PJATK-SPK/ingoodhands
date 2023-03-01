using System.Reflection;
using System.Text.Json;

namespace Core.Setup.ConfigSetup
{
    internal static class DefaultFileConfigLoader
    {
        public static readonly string GetRelativePathPrefix = "Configuration/Default/";

        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static AppConfig Load()
        {
            const string relativePath = "Configuration/Default/config.json";

            var json = LoadFileAsString(relativePath);

            if (string.IsNullOrWhiteSpace(json))
                throw new ConfigurationLoadingException("There is no default config file! Please: \n" +
                    "1. Set env variables.\n" +
                    "OR\n" +
                    "2. Provide json config file with default values (Configuration/Default/config.json).");

            var result = JsonSerializer.Deserialize<AppConfig>(json, _options)!;

            return result;
        }

        private static string? LoadFileAsString(string relativePath)
        {
            string path = GetConfigFullPath(relativePath);
            if (!File.Exists(path)) return null;
            var result = File.ReadAllText(path);
            return result;
        }

        public static string GetConfigFullPath(string relativePath, bool addRelativePathPrefix = false)
        {
            var path = relativePath;

            if (addRelativePathPrefix)
            {
                path = GetRelativePathPrefix + relativePath;
            }
            var location = GetCurrentLocation();
            var result =
                Path.Combine(
                    Path.GetDirectoryName(location)!,
                    path
                );
            return result;
        }

        private static string GetCurrentLocation()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var result = new Uri(location).LocalPath;
            return result.ToString();
        }
    }
}
