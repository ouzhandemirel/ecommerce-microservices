using System;
using NotificationService.Application.Interfaces.Repositories;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Persistence.Contexts;
using Shared.Infrastructure.Persistence;

namespace NotificationService.Infrastructure.Persistence.Repositories;

public class NotificationRepository : EfCoreRepository<Notification, Guid, NotificationDbContext>, INotificationRepository
{
    public NotificationRepository(NotificationDbContext context) : base(context)
    {
    }
}
