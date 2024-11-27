using System;
using NotificationService.Domain.Entities;
using Shared.Application.Persistence;

namespace NotificationService.Application.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product, Guid>
{

}
