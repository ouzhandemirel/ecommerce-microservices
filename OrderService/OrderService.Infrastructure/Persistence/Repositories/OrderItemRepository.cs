using System;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Persistence.Contexts;
using Shared.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Persistence.Repositories;

public class OrderItemRepository : EfCoreRepository<OrderItem, Guid, OrderDbContext>, IOrderItemRepository
{
	public OrderItemRepository(OrderDbContext context) : base(context)
	{
	}
}