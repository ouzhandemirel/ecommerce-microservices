using Shared.Domain.Abstractions;

namespace StockService.Domain.Events;

public class StockChangeEvent : IntegrationEvent
{
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
    public bool IsIncreased { get; set; }

    public StockChangeEvent(Guid productId, int amount, bool isIncreased)
    {
        ProductId = productId;
        Amount = amount;
        IsIncreased = isIncreased;
    }
}