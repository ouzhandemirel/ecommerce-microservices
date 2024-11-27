using StockService.Application.DTOs;

namespace StockService.Application.Interfaces.Services;

public interface IStockService
{
    Task<int> GetStock(Guid productId, CancellationToken cancellationToken);
    Task IncreaseStock(IncreaseStockCommand command, CancellationToken cancellationToken);
    Task ReduceStock(ReduceStockCommand command, CancellationToken cancellationToken);
}