using NotificationQueueConsumer;
using NotificationQueueConsumer.OrderNotificationEventHandler;
using NotificationService.Application;
using NotificationService.Infrastructure;
using EventHandler = NotificationQueueConsumer.OrderNotificationEventHandler.EventHandler;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<IEventHandler, EventHandler>();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHostedService<Consumer>();

var host = builder.Build();
host.Run();
