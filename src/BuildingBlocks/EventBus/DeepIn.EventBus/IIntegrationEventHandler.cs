using MassTransit;

namespace DeepIn.EventBus;

public interface IIntegrationEventHandler<in TIntegrationEvent> : IConsumer<TIntegrationEvent>
     where TIntegrationEvent : IntegrationEvent
{
}
