using Autofac;
using Core.Auth;
using Core.Autofac;
using Core.ConfigSetup;
using Core.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            InjectEntityFramework(builder);

            if (_registerWebApiSerivces)
            {
                RegisterWebApiServices(builder);
            }
        }

        private void InjectAppConfiguration(ContainerBuilder builder)
        {
            builder.RegisterInstance(ConfigurationReader.Get()).SingleInstance();
        }

        private void InjectEntityFramework(ContainerBuilder builder)
        {
            builder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseNpgsql(ConfigurationReader.Get().ConnectionStrings.Database);
                optionsBuilder.UseSnakeCaseNamingConvention();
                var result = new AppDbContext(optionsBuilder.Options);
                result.Database.Migrate();
                return result;
            }).InstancePerLifetimeScope();
        }

        private void RegisterWebApiServices(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterAsScoped<WebApiCurrentUserService>();
        }
    }
}
