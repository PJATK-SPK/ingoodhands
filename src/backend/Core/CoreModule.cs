using Autofac;
using Core.Services;
using Core.Setup;
using Core.Setup.Autofac;
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
            builder.RegisterAsScoped<GetCurrentUserService>();
            builder.RegisterAsScoped<RoleService>();
            builder.RegisterAsScoped<CounterService>();
        }
    }
}
