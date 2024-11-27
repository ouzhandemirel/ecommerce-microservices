using OrderService.Domain.Constants;
using OrderService.Domain.Events;
using Shared.Domain.Abstractions;
using Shared.Domain.Exceptions;

namespace OrderService.Domain.Entities;

public class Order : Entity<Guid>
{
    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }

    public ICollection<OrderItem> OrderItems { get; private set; } = null!;

    public Order() { }

    public Order(Guid customerId, List<OrderItem>? orderItems = null)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Status = OrderStatus.Created;

        if (orderItems != null)
        {
            OrderItems = orderItems;
        }
        else
        {
            OrderItems = [];
        }
    }

    public void AddOrderItem(Guid productId, int quantity)
    {
        var orderItem = new OrderItem(Id, productId, quantity);
        OrderItems.Add(orderItem);
    }

    public void PlaceOrder(bool withEvents = true)
    {
        if (Status != OrderStatus.Created)
        {
            throw new DomainException("Order must be in Created status to be placed");
        }

        if (OrderItems.Count == 0)
        {
            throw new DomainException("Order must have at least one item");
        }

        Status = OrderStatus.Placed;

        if (withEvents)
        {
            var orderPlacedEvent = new OrderEvent(
            Id,
            OrderItems.Select(x => new OrderEvent.OrderEventOrderItem(x.ProductId, x.Quantity)).ToList(),
            false);
            _integrationEvents.Add(orderPlacedEvent);

            var orderPlacedNotificationEvent = new OrderNotificationEvent(
                Id,
                CustomerId,
                OrderItems.Select(x => new OrderNotificationEvent.OrderNotificationEventOrderItem(x.ProductId, x.Quantity)).ToList(),
                false);
            _integrationEvents.Add(orderPlacedNotificationEvent);
        }

    }

    public void CancelOrder(bool withEvents = true)
    {
        if (Status != OrderStatus.Placed)
        {
            throw new DomainException("Order must be in Placed status to be cancelled");
        }

        Status = OrderStatus.Cancelled;

        if (withEvents)
        {
            var orderCancelledEvent = new OrderEvent(
                Id,
                OrderItems.Select(x => new OrderEvent.OrderEventOrderItem(x.ProductId, x.Quantity)).ToList(),
                true);
            _integrationEvents.Add(orderCancelledEvent);

            var orderCancelledNotificationEvent = new OrderNotificationEvent(
                Id,
                CustomerId,
                OrderItems.Select(x => new OrderNotificationEvent.OrderNotificationEventOrderItem(x.ProductId, x.Quantity)).ToList(),
                true);
            _integrationEvents.Add(orderCancelledNotificationEvent);
        }
    }

}