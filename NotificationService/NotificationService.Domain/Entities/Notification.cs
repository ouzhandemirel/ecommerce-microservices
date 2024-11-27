using System;
using NotificationService.Domain.Constants;
using Shared.Domain.Abstractions;
using Shared.Domain.Exceptions;

namespace NotificationService.Domain.Entities;

public class Notification : Entity<Guid>
{
    public Guid CustomerId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public NotificationStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? SentAt { get; private set; }

    public Customer Customer { get; private set; } = null!;

    public Notification()
    {
    }

    public Notification(Guid customerId, string title, string content)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Title = title;
        Content = content;
        Status = NotificationStatus.Pending;
    }

    public void MarkAsSent()
    {
        if (Status != NotificationStatus.Pending)
        {
            throw new DomainException("Only pending notifications can be marked as sent.");
        }

        Status = NotificationStatus.Sent;
        SentAt = DateTime.UtcNow;
    }
}
