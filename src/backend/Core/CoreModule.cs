using Autofac;
using Core.Setup;

namespace Core
{
    public class CoreModule : Module
    {
        private readonly SetupModule _setupModule;

        public CoreModule(bool registerWebApiSerivces)
        {
            _setupModule = new SetupModule(registerWebApiSerivces);
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
