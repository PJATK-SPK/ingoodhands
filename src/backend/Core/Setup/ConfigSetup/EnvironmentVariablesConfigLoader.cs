using System.Text;

namespace Core.Setup.ConfigSetup
{
    internal static class EnvironmentVariablesConfigLoader
    {
        public static bool IsAtLeast1EnvVariableSet
            => AllEnvironmentVariableNames.Any(c => Environment.GetEnvironmentVariable(c) != null);

        private static bool AllEnvVariableAreSet
            => AllEnvironmentVariableNames.All(c => Environment.GetEnvironmentVariable(c) != null);

        private static IEnumerable<string> NotSetEnvironmentVariableNames
            => AllEnvironmentVariableNames.Where(c => Environment.GetEnvironmentVariable(c) == null);

        private static IEnumerable<string> AllEnvironmentVariableNames
            => AppConfigMap.AppToEnvNames.Values.Concat(AppConfigMap.DbToEnvNames.Values);

        public static AppConfig Load()
        {
            if (!AllEnvVariableAreSet)
            {
                var list = NotSetEnvironmentVariableNames.ToList();
                var variablesToSet = string.Join('\n', list);
                throw new ConfigurationLoadingException($"Found {list.Count} missing env variable(s):\n{variablesToSet}");
            }

            var result = new AppConfig
            {
                OAuth2Authority = Environment.GetEnvironmentVariable(AppConfigMap.AppToEnvNames[nameof(AppConfig.OAuth2Authority)])!,
                OAuth2Audience = Environment.GetEnvironmentVariable(AppConfigMap.AppToEnvNames[nameof(AppConfig.OAuth2Audience)])!,
                OAuth2ClientId = Environment.GetEnvironmentVariable(AppConfigMap.AppToEnvNames[nameof(AppConfig.OAuth2ClientId)])!,
                OAuth2Scopes = Environment.GetEnvironmentVariable(AppConfigMap.AppToEnvNames[nameof(AppConfig.OAuth2Scopes)])!,
                DatabaseConnectionString = CreateConnectionString(),
                FrontendURL = Environment.GetEnvironmentVariable(AppConfigMap.AppToEnvNames[nameof(AppConfig.FrontendURL)])!,
                HashidsSalt = Environment.GetEnvironmentVariable(AppConfigMap.AppToEnvNames[nameof(AppConfig.HashidsSalt)])!,
            };

            return result;
        }

        private static string CreateConnectionString()
        {
            var sb = new StringBuilder();
            sb.Append($"User ID={Environment.GetEnvironmentVariable(AppConfigMap.DbToEnvNames["User ID"])};");
            sb.Append($"Password={Environment.GetEnvironmentVariable(AppConfigMap.DbToEnvNames["Password"])};");
            sb.Append($"Database={Environment.GetEnvironmentVariable(AppConfigMap.DbToEnvNames["Database"])};");
            sb.Append($"Host={AppConfigMap.DbToEnvNames["Host"]};");
            sb.Append($"Port={Environment.GetEnvironmentVariable(AppConfigMap.DbToEnvNames["Port"])};");
            sb.Append($"Connection Lifetime=0;Include Error Detail=true");

            throw new ArgumentException(sb.ToString());

            return sb.ToString();
        }
    }
}
