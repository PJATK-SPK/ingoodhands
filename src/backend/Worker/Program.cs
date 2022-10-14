using Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Service>();
    })
    .Build();

await host.RunAsync();
