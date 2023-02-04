using Autofac;
using Core.Autofac;
using Core.ConfigSetup;
using Microsoft.AspNetCore.Http;
using Core.WebApi.Auth;
using HashidsNet;

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
            InjectHashids(builder, ConfigurationReader.Get().HashidsSalt);

            if (_registerWebApiSerivces)
            {
                RegisterWebApiServices(builder);
            }
        }

        private void InjectHashids(ContainerBuilder builder, string salt)
        {
            builder.RegisterInstance(new Hashids(salt)).SingleInstance();
        }

        private void InjectAppConfiguration(ContainerBuilder builder)
        {
            builder.RegisterInstance(ConfigurationReader.Get()).SingleInstance();
        }

        private void RegisterWebApiServices(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterAsScoped<WebApiCurrentUserService>();
        }
    }
}
