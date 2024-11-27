using System;
using Shared.Domain.Abstractions;

namespace NotificationService.Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; set; } = string.Empty;

    public Product()
    {
    }
}
