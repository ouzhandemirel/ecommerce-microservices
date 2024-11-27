using System;
using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.UOW;
using OrderService.Infrastructure.Persistence.Contexts;
using Shared.Infrastructure.Messaging;
using Shared.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Persistence.UOW;

public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
{
    private IProductRepository? _productRepository;
    private IOrderRepository? _orderRepository;
    private IOrderItemRepository? _orderItemRepository;
    private readonly IServiceProvider _serviceProvider;
    
    public UnitOfWork(OrderDbContext dbContext,
        ICapPublisher eventBus, 
        IEventMapper eventMapper, 
        IServiceProvider serviceProvider) : base(dbContext, eventBus, eventMapper)
    {
        _serviceProvider = serviceProvider;
    }

    public IProductRepository ProductRepository =>  
        _productRepository ??= _serviceProvider.GetRequiredService<IProductRepository>();

    public IOrderRepository OrderRepository =>  
        _orderRepository ??= _serviceProvider.GetRequiredService<IOrderRepository>();

    public IOrderItemRepository OrderItemRepository =>  
        _orderItemRepository ??= _serviceProvider.GetRequiredService<IOrderItemRepository>();
}