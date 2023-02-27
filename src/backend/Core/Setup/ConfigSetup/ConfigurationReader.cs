namespace Core.Setup.ConfigSetup
{
    public static class ConfigurationReader
    {
        private static AppConfig? _cache;

        public static AppConfig Get()
        {
            if (_cache != null) return _cache;

            _cache = EnvironmentVariablesConfigLoader.IsAtLeast1EnvVariableSet
                    ? EnvironmentVariablesConfigLoader.Load()
                    : DefaultFileConfigLoader.Load();

            return _cache;
        }
    }
}
