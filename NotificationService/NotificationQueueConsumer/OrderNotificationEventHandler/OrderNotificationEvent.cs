using System;
using Shared.Domain.Abstractions;

namespace NotificationQueueConsumer.OrderNotificationEventHandler;

public class OrderNotificationEvent : IntegrationEvent
{
    public Guid OrderId { get; }
    public Guid CustomerId { get; }
    public List<OrderNotificationEventOrderItem> OrderItems { get; }
    public bool IsCancelled { get; }

    public OrderNotificationEvent(Guid orderId, Guid customerId, List<OrderNotificationEventOrderItem> orderItems, bool isCancelled)
    {
        OrderId = orderId;
        CustomerId = customerId;
        OrderItems = orderItems;
        IsCancelled = isCancelled;
    }

    public class OrderNotificationEventOrderItem
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public OrderNotificationEventOrderItem(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}