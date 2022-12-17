using Autofac;
using Core.Json;
using Core.Autofac;
using Core.Configuration.App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Text.Json;
using Core.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Core.ConfigSetup;
using Core.WebApi.Auth;

namespace Core.WebApi
{
    public static class WebApiBuilder
    {
        public static IHostBuilder Configure(this IHostBuilder hostBuilder, IEnumerable<Module> usedModules)
        {
            var serilogConfigPath = ConfigurationReader.GetConfigFullPath("serilog-api.json");
            var serilogConfig = new ConfigurationBuilder()
                  .AddJsonFile(
                      serilogConfigPath,
                      optional: false,
                      reloadOnChange: true)
                  .Build();

            hostBuilder.UseSerilog((context, config) => config.ReadFrom.Configuration(serilogConfig));
            hostBuilder.SetupAutofac(usedModules);

            return hostBuilder;
        }

        public static AppConfig Configure(this IServiceCollection services, AppConfig? kernelConfig = null)
        {
            if (kernelConfig == null)
                kernelConfig = ConfigurationReader.Get();

            services.AddCors(c =>
                c.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                })
            );
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonDateTimeNullConverter());
                });
            services.AddEndpointsApiExplorer();

            if (kernelConfig.Environment != ConfigurationEnvironment.Prd)
                services.SetupSwagger(kernelConfig);

            services.SetupAuth(kernelConfig);

            return kernelConfig;
        }

        public static AppConfig Configure(this IApplicationBuilder app, string urlPrefix, AppConfig? kernelConfig = null)
        {
            if (kernelConfig == null)
                kernelConfig = ConfigurationReader.Get();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.OAuthClientId(kernelConfig.Authorization.ClientId));
            app.UsePathBase(new PathString(urlPrefix));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            return kernelConfig;
        }
    }
}
