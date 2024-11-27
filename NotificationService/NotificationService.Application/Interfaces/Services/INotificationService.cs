using System;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Interfaces.Services;

public interface INotificationService
{
    Task Notify(Notification notification);
}
