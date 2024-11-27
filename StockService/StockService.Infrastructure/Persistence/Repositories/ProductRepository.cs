using Shared.Infrastructure.Persistence;
using StockService.Application.Interfaces.Repositories;
using StockService.Domain.Entities;
using StockService.Infrastructure.Persistence.Contexts;

namespace StockService.Infrastructure.Persistence.Repositories;

public class ProductRepository : EfCoreRepository<Product, Guid, StockDbContext>, IProductRepository
{
    public ProductRepository(StockDbContext context) : base(context)
    {
    }
}