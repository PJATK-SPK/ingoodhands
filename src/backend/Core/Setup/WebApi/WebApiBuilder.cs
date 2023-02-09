﻿using Autofac;
using Core.Setup.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using Core.Setup.Swagger;
using Core.Setup.WebApi.Auth;
using Core.Setup.Autofac;
using Core.Setup.ConfigSetup.App;
using Core.Setup.ConfigSetup;

namespace Core.Setup.WebApi
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

        public static AppConfig Configure(this IServiceCollection services, AppConfig? config = null)
        {
            if (config == null)
                config = ConfigurationReader.Get();

            services.AddCors(c =>
                c.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyMethod();
                    builder.WithOrigins(config.Urls.Frontend.Substring(0, config.Urls.Frontend.Length - 1));
                    builder.AllowAnyHeader();
                })
            );
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonDateTimeNullConverter());
                });
            services.AddEndpointsApiExplorer();
            services.SetupSwagger(config);
            services.SetupAuth(config);

            return config;
        }

        public static AppConfig Configure(this IApplicationBuilder app, string urlPrefix, AppConfig? config = null)
        {
            if (config == null)
                config = ConfigurationReader.Get();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.OAuthClientId(config.Authorization.ClientId));
            app.UsePathBase(new PathString(urlPrefix));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            return config;
        }
    }
}