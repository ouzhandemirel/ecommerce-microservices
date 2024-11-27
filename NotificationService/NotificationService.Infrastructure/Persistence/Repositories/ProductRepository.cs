using System;
using NotificationService.Application.Interfaces.Repositories;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Persistence.Contexts;
using Shared.Infrastructure.Persistence;

namespace NotificationService.Infrastructure.Persistence.Repositories;

public class ProductRepository : EfCoreRepository<Product, Guid, NotificationDbContext>, IProductRepository
{
    public ProductRepository(NotificationDbContext context) : base(context)
    {
    }
}
