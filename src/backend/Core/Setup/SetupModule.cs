using Autofac;
using Microsoft.AspNetCore.Http;
using HashidsNet;
using Core.Setup.WebApi.Auth;
using Core.Setup.Autofac;
using Core.Setup.ConfigSetup;

namespace Core.Setup
{
    /// <summary>
    /// Injects: <br/>
    /// - Entity Framework 
    /// - AppConfig
    /// </summary>
    public class SetupModule
    {
        private readonly bool _registerWebApiServices;

        public SetupModule(bool registerWebApiSerivces)
        {
            _registerWebApiServices = registerWebApiSerivces;
        }

        public void RegisterAll(ContainerBuilder builder)
        {
            InjectAppConfiguration(builder);
            AutofacPostgresDbContextInjector.Inject(builder, ConfigurationReader.Get().ConnectionStrings.Database);
            InjectHashids(builder, ConfigurationReader.Get().HashidsSalt);

            if (_registerWebApiServices)
            {
                RegisterWebApiServices(builder);
            }
        }

        private static void InjectHashids(ContainerBuilder builder, string salt)
        {
            builder.RegisterInstance(new Hashids(salt, 5)).SingleInstance();
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
