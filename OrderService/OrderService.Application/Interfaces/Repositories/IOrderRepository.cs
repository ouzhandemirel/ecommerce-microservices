using System;
using OrderService.Domain.Entities;
using Shared.Application.Persistence;

namespace OrderService.Application.Interfaces.Repositories;

public interface IOrderRepository : IRepository<Order, Guid>
{

}
