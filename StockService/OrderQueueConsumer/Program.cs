using OrderQueueConsumer;
using StockService.Application;
using StockService.Infrastructure;
using EventHandler = OrderQueueConsumer.OrderEventHandler.EventHandler;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<EventHandler>();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHostedService<Consumer>();

var host = builder.Build();
host.Run();
