using System;
using NotificationService.Application.Interfaces.Repositories;
using Shared.Application.Persistence;

namespace NotificationService.Application.Interfaces.UOW;

public interface IUnitOfWork : IUnitOfWorkBase
{
    IProductRepository ProductRepository { get; }
    INotificationRepository NotificationRepository { get; }
    ICustomerRepository CustomerRepository { get; }
}
