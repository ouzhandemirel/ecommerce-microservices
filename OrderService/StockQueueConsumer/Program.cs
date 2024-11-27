using OrderService.Application;
using OrderService.Infrastructure;
using StockQueueConsumer;
using StockQueueConsumer.StockChangeEventHandler;
using EventHandler = StockQueueConsumer.StockChangeEventHandler.EventHandler;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<IEventHandler, EventHandler>();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHostedService<Consumer>();

var host = builder.Build();
host.Run();
