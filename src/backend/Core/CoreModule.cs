using Autofac;
using Core.Setup;
using Core.Setup.Enums;

namespace Core
{
    public class CoreModule : Module
    {
        private readonly SetupModule _setupModule;

        public CoreModule(WebApiUserProviderType userProviderType)
        {
            _setupModule = new SetupModule(userProviderType);
        }

        protected override void Load(ContainerBuilder builder)
        {
            _setupModule.RegisterAll(builder);
            RegisterServices(builder);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            // Will be used in future
        }
    }
}
