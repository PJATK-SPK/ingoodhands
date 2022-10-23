using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.ConfigSetup;
using Serilog;
using System.Collections.Specialized;
using Worker;

var builder = Host.CreateDefaultBuilder(args);

var serilogConfigPath = ConfigurationReader.GetConfigFullPath("serilog-service.json");
var serilogConfig = new ConfigurationBuilder()
    .AddJsonFile(
        serilogConfigPath,
        optional: false,
        reloadOnChange: true)
    .Build();

builder.UseSerilog((context, config) => config.ReadFrom.Configuration(serilogConfig));
builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.ConfigureServices((context, services) =>
{
    builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        foreach (var module in UsedModules.List)
            containerBuilder.RegisterModule(module);
    });

    services.AddHostedService<Service>();
});

var host = builder.Build();
await host.RunAsync();