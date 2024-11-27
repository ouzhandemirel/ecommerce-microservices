using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Interfaces.Notifications;
using NotificationService.Application.Interfaces.Repositories;
using NotificationService.Application.Interfaces.UOW;
using NotificationService.Infrastructure.Notifiers;
using NotificationService.Infrastructure.Persistence.Contexts;
using NotificationService.Infrastructure.Persistence.Repositories;
using NotificationService.Infrastructure.Persistence.UOW;
using Shared.Infrastructure.Messaging;

namespace NotificationService.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NotificationDbContext>();
        
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IEmailNotifier, EmailNotifier>();
        services.AddScoped<ISmsNotifier, SmsNotifier>();

        services.AddSingleton<IEventMapper, EventMapper>();

        services.AddCap(x =>
        {
            x.UseEntityFramework<NotificationDbContext>();
            x.UsePostgreSql(configuration.GetConnectionString("NotificationDb"));
            x.UseRabbitMQ(options =>
            {
                options.HostName = configuration["RabbitMqConfiguration:Host"];
            });
            x.FailedRetryCount = 5;
            x.FailedThresholdCallback = (message) =>
            {
                
            };
        });
    }
}
