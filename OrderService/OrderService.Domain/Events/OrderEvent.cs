using Shared.Domain.Abstractions;

namespace OrderService.Domain.Events;

public class OrderEvent : IntegrationEvent
{
    public Guid OrderId { get; }
    public List<OrderEventOrderItem> OrderItems { get; }
    public bool IsCancelled { get; }

    public OrderEvent(Guid orderId, List<OrderEventOrderItem> orderItems, bool isCancelled)
    {
        OrderId = orderId;
        OrderItems = orderItems;
        IsCancelled = isCancelled;
    }

    public class OrderEventOrderItem
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public OrderEventOrderItem(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}