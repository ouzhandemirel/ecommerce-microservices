using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Messaging;
using StockService.Application.Interfaces.Repositories;
using StockService.Application.Interfaces.UOW;
using StockService.Domain.Events;
using StockService.Infrastructure.Persistence.Contexts;
using StockService.Infrastructure.Persistence.Repositories;
using StockService.Infrastructure.Persistence.UOW;

namespace StockService.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Registering database context
        services.AddDbContext<StockDbContext>();

        // Registering repositories and unit of work
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Registering event mapper
        services.AddSingleton<IEventMapper, EventMapper>(x =>
        {
            var eventMapper = new EventMapper();
            eventMapper.RegisterMapping<StockChangeEvent>("stock.queue");
            return eventMapper;
        });

        // Registering CAP
        services.AddCap(x =>
        {
            x.UseEntityFramework<StockDbContext>();
            x.UsePostgreSql(configuration.GetConnectionString("StockDb"));
            x.UseRabbitMQ(options =>
            {
                options.HostName = configuration["RabbitMqConfiguration:Host"];
            });
            x.FailedRetryCount = 5;
        });
    }
}