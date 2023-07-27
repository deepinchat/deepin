using DeepIn.Chatting.Application.IntegrationEvents.Events;
using DeepIn.Chatting.Application.Queries;
using DeepIn.Chatting.SignalrHub.Hubs;
using DeepIn.EventBus;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace DeepIn.Chatting.SignalrHub.IntegrationEvents.EventHandling
{
    public class PushMessageIntegrationEventHandler : IIntegrationEventHandler<PushMessageIntegrationEvent>
    {
        private readonly ILogger _logger;
        private readonly IChatQueries _chatQueries;
        private readonly IHubContext<ChatsHub> _chatsHub;
        public PushMessageIntegrationEventHandler(
            ILogger<PushMessageIntegrationEventHandler> logger,
            IChatQueries chatQueries,
            IHubContext<ChatsHub> chatsHub)
        {
            _chatsHub = chatsHub;
            _chatQueries = chatQueries;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<PushMessageIntegrationEvent> context)
        {
            try
            {
                var userIds = await _chatQueries.GetChatUserIds(context.Message.ChatId);
                await _chatsHub.Clients.Users(userIds).SendAsync("new_message", context.Message.MessageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Event handling failed, Message was not pushed.");
            }
        }
    }
}
