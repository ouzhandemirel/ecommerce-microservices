namespace Shared.Application.Persistence;

public interface IUnitOfWorkBase
{
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
}