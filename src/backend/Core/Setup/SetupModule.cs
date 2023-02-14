using Autofac;
using Microsoft.AspNetCore.Http;
using HashidsNet;
using Core.Setup.WebApi.Auth;
using Core.Setup.Autofac;
using Core.Setup.ConfigSetup;
using Core.Setup.Enums;
using Core.Setup.WebApi.Worker;

namespace Core.Setup
{
    /// <summary>
    /// Injects: <br/>
    /// - Entity Framework 
    /// - AppConfig
    /// </summary>
    public class SetupModule
    {
        private readonly WebApiUserProviderType _userProviderType;

        public SetupModule(WebApiUserProviderType userProviderType)
        {
            _userProviderType = userProviderType;
        }

        public void RegisterAll(ContainerBuilder builder)
        {
            InjectAppConfiguration(builder);
            AutofacPostgresDbContextInjector.Inject(builder, ConfigurationReader.Get().ConnectionStrings.Database);
            InjectHashids(builder, ConfigurationReader.Get().HashidsSalt);

            if (_userProviderType == WebApiUserProviderType.ProvideByLoggedAuth0User)
            {
                RegisterWebApiUserByAuth0Services(builder);
            }
            else if (_userProviderType == WebApiUserProviderType.ProvideServiceUser)
            {
                RegisterWebApiServiceUser(builder);
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

        private static void RegisterWebApiUserByAuth0Services(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterAsScoped<WebApiCurrentUserService>();
        }

        private static void RegisterWebApiServiceUser(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<WorkerCurrentUserService>();
        }
    }
}
