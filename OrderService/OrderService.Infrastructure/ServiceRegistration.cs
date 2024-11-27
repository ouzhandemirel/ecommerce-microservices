using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.UOW;
using OrderService.Domain.Events;
using OrderService.Infrastructure.Persistence.Contexts;
using OrderService.Infrastructure.Persistence.Repositories;
using OrderService.Infrastructure.Persistence.UOW;
using Shared.Infrastructure.Messaging;

namespace OrderService.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>();

        // Registering repositories and unit of work
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Registering event mapper
        services.AddSingleton<IEventMapper, EventMapper>(x =>
        {
            var eventMapper = new EventMapper();
            eventMapper.RegisterMapping<OrderEvent>("order.queue");
            eventMapper.RegisterMapping<OrderNotificationEvent>("order.notifications.queue");
            return eventMapper;
        });

        // Registering CAP
        services.AddCap(x =>
        {
            x.UseEntityFramework<OrderDbContext>();
            x.UsePostgreSql(configuration.GetConnectionString("OrderDb"));
            x.UseRabbitMQ(options =>
            {
                options.HostName = configuration["RabbitMqConfiguration:Host"];
            });
            x.FailedRetryCount = 5;
        });
    }
}
