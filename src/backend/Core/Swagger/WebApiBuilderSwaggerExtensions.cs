using Core.Configuration.App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Core.Swagger
{
    internal static class WebApiBuilderSwaggerExtensions
    {
        public static void SetupSwagger(this IServiceCollection services, AppConfig config)
        {
            var auth = config.Authorization;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Using the Authorization header with the Bearer scheme.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            Scopes = auth.Scopes.Split(" ").ToDictionary(c => c, c => c),
                            AuthorizationUrl = new Uri($"{auth.Authority}authorize?audience={auth.Audience}")
                        }
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }
    }
}
