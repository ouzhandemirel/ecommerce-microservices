using DotNetCore.CAP;
using OrderService.Application.Interfaces.UOW;
using Shared.Application.Exceptions;

namespace StockQueueConsumer.StockChangeEventHandler;

public class EventHandler : ICapSubscribe, IEventHandler
{
    private readonly IUnitOfWork _unitOfWork;
    public EventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [CapSubscribe("stock.queue")]
    public async Task Handle(StockChangeEvent stockChangeEvent)
    {
        await _unitOfWork.BeginTransactionAsync();

        var product = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == stockChangeEvent.ProductId)
            ?? throw new BusinessException($"Product with id {stockChangeEvent.ProductId} not found.");

        if (stockChangeEvent.IsIncreased)
        {
            product.IncreaseStock(stockChangeEvent.Amount);
        }
        else
        {
            product.ReduceStock(stockChangeEvent.Amount);
        }

        await _unitOfWork.ProductRepository.UpdateAsync(product);
        await _unitOfWork.CommitTransactionAsync();
    }
}
