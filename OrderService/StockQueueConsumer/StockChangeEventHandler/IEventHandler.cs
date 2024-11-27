using System;

namespace StockQueueConsumer.StockChangeEventHandler;

public interface IEventHandler
{
    Task Handle(StockChangeEvent stockChangeEvent);
}
