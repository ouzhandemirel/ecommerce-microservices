using System;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Interfaces.Notifications;

public interface INotifier
{
    Task NotifyAsync(string receiver, string title, string content);
}
