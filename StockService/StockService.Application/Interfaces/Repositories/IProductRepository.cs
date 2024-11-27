using Shared.Application.Persistence;
using StockService.Domain.Entities;

namespace StockService.Application.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product, Guid>
{
}