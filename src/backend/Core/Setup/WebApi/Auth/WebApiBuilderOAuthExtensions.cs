using Core.Setup.ConfigSetup.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Setup.WebApi.Auth
{
    internal static class WebApiBuilderOAuthExtensions
    {
        public static void SetupAuth(this IServiceCollection services, AppConfig config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = config.Authorization.Authority;
                options.Audience = config.Authorization.Audience;
            });
        }
    }
}
