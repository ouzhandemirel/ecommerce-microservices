using System;
using NotificationService.Domain.Entities;
using Shared.Application.Persistence;

namespace NotificationService.Application.Interfaces.Repositories;

public interface INotificationRepository : IRepository<Notification, Guid>
{

}
