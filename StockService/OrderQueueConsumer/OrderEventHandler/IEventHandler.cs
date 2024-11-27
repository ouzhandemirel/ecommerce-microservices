namespace OrderQueueConsumer.OrderEventHandler;

public interface IEventHandler
{
    Task Handle(OrderEvent orderEvent);
}
