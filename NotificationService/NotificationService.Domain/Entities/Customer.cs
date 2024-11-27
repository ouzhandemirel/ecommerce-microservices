using System;
using Shared.Domain.Abstractions;

namespace NotificationService.Domain.Entities;

public class Customer : Entity<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public string Surname { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;

    public ICollection<Notification> Notifications { get; set; } = null!;

    public Customer()
    {
    }

    public Customer(string name, string surname, string email, string phoneNumber)
    {
        Id = Guid.NewGuid();
        Name = name;
        Surname = surname;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}
