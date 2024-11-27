using System;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Persistence.Contexts;
using Shared.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Persistence.Repositories;

public class ProductRepository : EfCoreRepository<Product, Guid, OrderDbContext>, IProductRepository
{
	public ProductRepository(OrderDbContext context) : base(context)
	{
	}
}
