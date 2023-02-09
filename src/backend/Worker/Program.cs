using Core.Setup.WebApi;
using Worker;

var builder = WebApplication.CreateBuilder(args);

WebApiBuilder.Configure(builder.Host, UsedModules.List);
WebApiBuilder.Configure(builder.Services);

var app = builder.Build();
WebApiBuilder.Configure(app, "/");

app.Run();
