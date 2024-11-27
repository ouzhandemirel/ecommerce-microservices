using Shared.Application.Persistence;
using StockService.Application.Interfaces.Repositories;

namespace StockService.Application.Interfaces.UOW;

public interface IUnitOfWork : IUnitOfWorkBase
{
    IProductRepository ProductRepository { get; }
}