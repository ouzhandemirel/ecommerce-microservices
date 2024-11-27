using Shared.Domain.Abstractions;
using Shared.Domain.Exceptions;
using StockService.Domain.Events;

namespace StockService.Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public int Barcode { get; private set; }
    public int Quantity { get; private set; }


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
        Barcode = barcode;
    }

    public void IncreaseStock(int quantity, bool withEvents = true)
    {
        if (0 >= quantity)
        {
            throw new DomainException("Added quantity must be greater than 0");
        }
        Quantity += quantity;

        if (withEvents)
        {
            _integrationEvents.Add(new StockChangeEvent(Id, quantity, true));
        }
    }

    public void ReduceStock(int quantity, bool withEvents = true)
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

        if (withEvents)
        {
            _integrationEvents.Add(new StockChangeEvent(Id, quantity, false));
        }
    }
}