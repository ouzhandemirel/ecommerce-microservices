using Shared.Domain.Interfaces;

namespace Shared.Domain.Abstractions;

public abstract class Entity<TId> : IHasIntegrationEvents
{
    public TId Id { get; set; } = default!;

    protected List<IntegrationEvent> _integrationEvents = [];
    public IReadOnlyCollection<IntegrationEvent> IntegrationEvents => _integrationEvents.AsReadOnly();

    public void ClearIntegrationEvents()
    {
        _integrationEvents.Clear();
    }
}