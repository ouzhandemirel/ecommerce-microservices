using System;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Persistence.Contexts;
using Shared.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Persistence.Repositories;

public class OrderRepository : EfCoreRepository<Order, Guid, OrderDbContext>, IOrderRepository
{
	public OrderRepository(OrderDbContext context) : base(context)
	{
	}
}