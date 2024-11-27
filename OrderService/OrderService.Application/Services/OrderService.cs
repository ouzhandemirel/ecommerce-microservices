using Microsoft.EntityFrameworkCore;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces.Services;
using OrderService.Application.Interfaces.UOW;
using OrderService.Domain.Entities;
using Shared.Application.Exceptions;
using Shared.Domain.Exceptions;

namespace OrderService.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateOrder(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        var products = await _unitOfWork.ProductRepository.GetListAsync(x => command.OrderLineItems.Select(x => x.ProductId).Contains(x.Id));
        var unknownProductIds = command.OrderLineItems.Select(x => x.ProductId).Except(products.Select(x => x.Id));

        if (unknownProductIds.Any())
        {
            throw new DomainException($"Product(s) with id(s) {unknownProductIds} not found.");
        }

        var order = new Order(command.CustomerId);

        foreach (var item in command.OrderLineItems)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == item.ProductId)
                ?? throw new BusinessException($"Product with id {item.ProductId} not found.");

            if (product.Quantity < item.Quantity)
            {
                throw new BusinessException($"Product with id {item.ProductId} is out of stock.");
            }

            order.AddOrderItem(item.ProductId, item.Quantity);
            product.ReduceStock(item.Quantity);

            await _unitOfWork.ProductRepository.UpdateAsync(product);
        }

        order.PlaceOrder();
        var orderId = await _unitOfWork.OrderRepository.AddAsync(order);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);
    }

    public async Task CancelOrder(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        var order = await _unitOfWork.OrderRepository.GetAsync(x => x.Id == command.OrderId, x => x.Include(x => x.OrderItems))
            ?? throw new Exception($"Order with id {command.OrderId} not found.");
            
        foreach(var item in order.OrderItems)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == item.ProductId)
                ?? throw new BusinessException($"Product with id {item.ProductId} not found.");
            product.IncreaseStock(item.Quantity);

            await _unitOfWork.ProductRepository.UpdateAsync(product);
        }

        order.CancelOrder();
        await _unitOfWork.OrderRepository.UpdateAsync(order);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);
    }
}
