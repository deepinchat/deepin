using DeepIn.Chatting.SignalrHub.Hubs;
using DeepIn.EventBus;
using DeepIn.EventBus.Shared.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace DeepIn.Chatting.SignalrHub.IntegrationEvents.EventHandling
{
    public class PushMessageIntegrationEventHandler : IIntegrationEventHandler<PushMessageIntegrationEvent>
    {
        private readonly ILogger _logger;
        private readonly IHubContext<ChatsHub> _chatsHub;
        public PushMessageIntegrationEventHandler(
            ILogger<PushMessageIntegrationEventHandler> logger,
            IHubContext<ChatsHub> chatsHub)
        {
            _chatsHub = chatsHub;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<PushMessageIntegrationEvent> context)
        {
            try
            {
                await _chatsHub.Clients.Group(context.Message.ChatId).SendAsync("new_message", context.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Event handling failed, Message was not pushed.");
            }
        }
    }
}
