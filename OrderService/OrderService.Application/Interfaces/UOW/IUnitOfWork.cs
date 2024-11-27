using System;
using OrderService.Application.Interfaces.Repositories;
using Shared.Application.Persistence;

namespace OrderService.Application.Interfaces.UOW;

public interface IUnitOfWork : IUnitOfWorkBase
{
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderItemRepository OrderItemRepository { get; }
}
