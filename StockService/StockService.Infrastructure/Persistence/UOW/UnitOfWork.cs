using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Messaging;
using Shared.Infrastructure.Persistence;
using StockService.Application.Interfaces.Repositories;
using StockService.Application.Interfaces.UOW;
using StockService.Infrastructure.Persistence.Contexts;

namespace StockService.Infrastructure.Persistence.UOW;

public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
{
    private IProductRepository? _productRepository;
    private readonly IServiceProvider _serviceProvider;
    
    public UnitOfWork(StockDbContext dbContext,
        ICapPublisher eventBus, 
        IEventMapper eventMapper, 
        IServiceProvider serviceProvider) : base(dbContext, eventBus, eventMapper)
    {
        _serviceProvider = serviceProvider;
    }

    public IProductRepository ProductRepository =>  
        _productRepository ??= _serviceProvider.GetRequiredService<IProductRepository>();
}