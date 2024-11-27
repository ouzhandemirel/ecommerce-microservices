using Shared.Domain.Abstractions;
using Shared.Domain.Exceptions;

namespace OrderService.Domain.Entities;

public class OrderItem : Entity<Guid>
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public Order Order { get; private set; } = null!;
    public Product Product { get; private set; } = null!;

    public OrderItem() { }

    public OrderItem(Guid orderId, Guid productId, int quantity)
    {
        if (quantity <= 0)
        {
            throw new DomainException("Quantity must be greater than 0");
        }

        Id = Guid.NewGuid();
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }
}