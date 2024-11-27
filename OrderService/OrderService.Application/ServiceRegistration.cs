using System;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces.Services;

namespace OrderService.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, Services.OrderService>();
    }
}