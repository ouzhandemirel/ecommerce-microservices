using System;
using DotNetCore.CAP;
using StockService.Application.Interfaces.UOW;
using StockService.Domain.Entities;

namespace OrderQueueConsumer.OrderEventHandler;

public class EventHandler : ICapSubscribe, IEventHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public EventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [CapSubscribe("order.queue")]
    public async Task Handle(OrderEvent orderEvent)
    {
        await _unitOfWork.BeginTransactionAsync();

        List<Product> updatedProducts = [];
        var orderProducts = await _unitOfWork.ProductRepository.GetListAsync(x => orderEvent.OrderItems.Select(x => x.ProductId).Contains(x.Id));

        foreach (var product in orderProducts)
        {
            var orderItem = orderEvent.OrderItems.FirstOrDefault(x => x.ProductId == product.Id)
                ?? throw new Exception($"Product info with id {product.Id} not found in order stock data");

            if (orderEvent.IsCancelled)
            {
                product.IncreaseStock(orderItem.Quantity, withEvents: false);
            }
            else
            {
                product.ReduceStock(orderItem.Quantity, withEvents: false);
            }

            updatedProducts.Add(product);
        }

        await _unitOfWork.ProductRepository.UpdateRangeAsync(updatedProducts);
        await _unitOfWork.CommitTransactionAsync();
    }
}
