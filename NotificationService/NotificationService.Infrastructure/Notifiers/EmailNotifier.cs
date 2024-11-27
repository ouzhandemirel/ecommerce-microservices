using System;
using NotificationService.Application.Interfaces.Notifications;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Notifiers;

public class EmailNotifier : IEmailNotifier
{
    public EmailNotifier()
    {
    }

    public Task NotifyAsync(string receiver, string title, string content)
    {
        // Email sending logic here
        return Task.CompletedTask;
    }
}
