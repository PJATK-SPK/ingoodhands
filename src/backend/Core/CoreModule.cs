using Autofac;
using Core.Autofac;
using Core.ConfigSetup;
using Microsoft.AspNetCore.Http;
using Core.WebApi.Auth;

namespace Core
{
    /// <summary>
    /// Injects: <br/>
    /// - Entity Framework 
    /// - AppConfig
    /// </summary>
    public class CoreModule : Module
    {
        private readonly bool _registerWebApiSerivces;

        public CoreModule(bool registerWebApiSerivces)
        {
            _registerWebApiSerivces = registerWebApiSerivces;
        }

        protected override void Load(ContainerBuilder builder)
        {
            InjectAppConfiguration(builder);
            AutofacPostgresDbContextInjector.Inject(builder, ConfigurationReader.Get().ConnectionStrings.Database);

            if (_registerWebApiSerivces)
            {
                RegisterWebApiServices(builder);
            }
        }

        private static void InjectAppConfiguration(ContainerBuilder builder)
        {
            builder.RegisterInstance(ConfigurationReader.Get()).SingleInstance();
        }

        private static void RegisterWebApiServices(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterAsScoped<WebApiCurrentUserService>();
        }
    }
}
