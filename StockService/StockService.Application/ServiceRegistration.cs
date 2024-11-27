using Microsoft.Extensions.DependencyInjection;
using StockService.Application.Interfaces.Repositories;
using StockService.Application.Interfaces.Services;
using StockService.Application.Interfaces.UOW;

namespace StockService.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Registering services
        services.AddScoped<IStockService, Services.StockService>();
    }
}