using System;

namespace NotificationQueueConsumer.OrderNotificationEventHandler;

public interface IEventHandler
{
    Task Handle(OrderNotificationEvent orderNotificationEvent);
}
