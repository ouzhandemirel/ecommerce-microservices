using Shared.Domain.Abstractions;

namespace Shared.Infrastructure.Messaging;

public interface IEventMapper
{
    void RegisterMapping<TEvent>(string routingKey) where TEvent : IntegrationEvent;
    HashSet<string> GetMappings<TEvent>(TEvent @event) where TEvent : IntegrationEvent;
}