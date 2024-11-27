using System;
using NotificationService.Application.Interfaces.Notifications;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Notifiers;

public class SmsNotifier : ISmsNotifier
{
    public SmsNotifier()
    {
    }

    public Task NotifyAsync(string receiver, string title, string content)
    {
        // Sms sending logic here
        return Task.CompletedTask;
    }
}
