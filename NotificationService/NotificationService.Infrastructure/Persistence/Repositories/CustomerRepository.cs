using System;
using NotificationService.Application.Interfaces.Repositories;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Persistence.Contexts;
using Shared.Infrastructure.Persistence;

namespace NotificationService.Infrastructure.Persistence.Repositories;

public class CustomerRepository : EfCoreRepository<Customer, Guid, NotificationDbContext>, ICustomerRepository
{
    public CustomerRepository(NotificationDbContext context) : base(context)
    {
    }
}
