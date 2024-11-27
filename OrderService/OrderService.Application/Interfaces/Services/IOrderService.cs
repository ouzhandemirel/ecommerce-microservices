using System;
using OrderService.Application.DTOs;

namespace OrderService.Application.Interfaces.Services;

public interface IOrderService
{
    Task CreateOrder(CreateOrderCommand command, CancellationToken cancellationToken);
    Task CancelOrder(CancelOrderCommand command, CancellationToken cancellationToken);
    
}
