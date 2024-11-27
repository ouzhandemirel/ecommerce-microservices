using System;
using Shared.Domain.Abstractions;

namespace StockQueueConsumer.StockChangeEventHandler;

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