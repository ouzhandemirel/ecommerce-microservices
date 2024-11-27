using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Application.Persistence;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Messaging;

namespace Shared.Infrastructure.Persistence;

public class UnitOfWorkBase : IUnitOfWorkBase
{
    private readonly DbContext _dbContext;
    private IDbContextTransaction? _transaction;
    private readonly ICapPublisher _eventBus;
    private readonly IEventMapper _eventMapper;

    public UnitOfWorkBase(DbContext dbContext, ICapPublisher eventBus, IEventMapper eventMapper)
    {
        _dbContext = dbContext;
        _eventBus = eventBus;
        _eventMapper = eventMapper;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync(_eventBus, autoCommit: false, cancellationToken: cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("A transaction must be started before committing");
        }

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
            await PublishIntegrationEventsAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    private async Task DisposeTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    private async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
    }

    private async Task PublishIntegrationEventsAsync(CancellationToken cancellationToken)
    {
        var integrationEvents = _dbContext.ChangeTracker.Entries<IHasIntegrationEvents>()
            .SelectMany(x => x.Entity.IntegrationEvents)
            .ToList();

        foreach (var integrationEvent in integrationEvents)
        {
            var routingKeys = _eventMapper.GetMappings(integrationEvent);

            foreach (var routingKey in routingKeys)
            {
                await _eventBus.PublishAsync(
                    routingKey,
                    integrationEvent,
                    cancellationToken: cancellationToken);
            }
        }

        _dbContext.ChangeTracker.Entries<IHasIntegrationEvents>()
            .ToList()
            .ForEach(x => x.Entity.ClearIntegrationEvents());
    }
}