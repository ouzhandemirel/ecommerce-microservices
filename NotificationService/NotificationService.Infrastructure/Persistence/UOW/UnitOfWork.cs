using System;
using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Interfaces.Repositories;
using NotificationService.Application.Interfaces.UOW;
using NotificationService.Infrastructure.Persistence.Contexts;
using Shared.Infrastructure.Messaging;
using Shared.Infrastructure.Persistence;

namespace NotificationService.Infrastructure.Persistence.UOW;

public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
{
    private IProductRepository? _productRepository;
    private INotificationRepository? _notificationRepository;
    private ICustomerRepository? _customerRepository;
    private readonly IServiceProvider _serviceProvider;
    
    public UnitOfWork(NotificationDbContext dbContext,
        ICapPublisher eventBus, 
        IEventMapper eventMapper, 
        IServiceProvider serviceProvider) : base(dbContext, eventBus, eventMapper)
    {
        _serviceProvider = serviceProvider;
    }

    public IProductRepository ProductRepository =>  
        _productRepository ??= _serviceProvider.GetRequiredService<IProductRepository>();

    public INotificationRepository NotificationRepository =>
        _notificationRepository ??= _serviceProvider.GetRequiredService<INotificationRepository>();

    public ICustomerRepository CustomerRepository =>
        _customerRepository ??= _serviceProvider.GetRequiredService<ICustomerRepository>();
}