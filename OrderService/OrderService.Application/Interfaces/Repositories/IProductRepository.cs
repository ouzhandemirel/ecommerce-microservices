using System;
using OrderService.Domain.Entities;
using Shared.Application.Persistence;

namespace OrderService.Application.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product, Guid>
{
}
