using OrderService.Domain.Constants;
using OrderService.Domain.Events;
using Shared.Domain.Abstractions;
using Shared.Domain.Exceptions;

namespace OrderService.Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public int Quantity { get; private set; }

    public ICollection<OrderItem> OrderItems { get; private set; } = null!;
    

    public Product() { }

    public Product(string name, int barcode, int quantity)
    {
        if (quantity < 0)
        {
            throw new DomainException("Quantity must be greater than or equal to 0");
        }

        Id = Guid.NewGuid();
        Name = name;
        Quantity = quantity;
    }

    public void IncreaseStock(int quantity)
    {
        if (0 >= quantity)
        {
            throw new DomainException("Added quantity must be greater than 0");
        }
        Quantity += quantity;
    }

    public void ReduceStock(int quantity)
    {
        if (0 >= quantity)
        {
            throw new DomainException("Reduced quantity must be greater than 0");
        }

        if (Quantity < quantity)
        {
            throw new DomainException("Insufficient stock");
        }
        Quantity -= quantity;
    }
}