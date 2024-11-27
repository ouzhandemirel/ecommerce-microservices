using Shared.Domain.Abstractions;

namespace Shared.Domain.Interfaces;

public interface IHasIntegrationEvents
{
    IReadOnlyCollection<IntegrationEvent> IntegrationEvents { get; }
    void ClearIntegrationEvents();
}