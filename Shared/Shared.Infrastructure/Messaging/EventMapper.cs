using Shared.Domain.Abstractions;

namespace Shared.Infrastructure.Messaging;

public sealed class EventMapper : IEventMapper
{
    private readonly Dictionary<Type, HashSet<string>> _eventMappings = [];

    public EventMapper() { }
    
    public void RegisterMapping<TEvent>(string routingKey) where TEvent : IntegrationEvent
    {
        if (!_eventMappings.ContainsKey(typeof(TEvent)))
        {
            _eventMappings[typeof(TEvent)] = [];
        }
        _eventMappings[typeof(TEvent)].Add(routingKey);
    }
    
    public HashSet<string> GetMappings<TEvent>(TEvent @event) where TEvent : IntegrationEvent
    {
        var eventType = @event.GetType();
        if (_eventMappings.TryGetValue(eventType, out var mapping))
        {
            return mapping;
        }

        throw new InvalidOperationException($"Event mapping for type {eventType.Name} not found");
    }
}