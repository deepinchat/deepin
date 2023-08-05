using DeepIn.EventBus;
using DeepIn.EventBus.Shared.Events;
using DeepIn.Messaging.API.Services;
using MassTransit;

namespace DeepIn.Messaging.API.Application.IntegrationEvents.EventHandling
{
    public class SaveMessageIntegrationEventHandler : IIntegrationEventHandler<SaveMessageIntegrationEvent>
    {
        private readonly ILogger _logger;
        private readonly IMessageService _messageService;
        public SaveMessageIntegrationEventHandler(
            ILogger<SaveMessageIntegrationEventHandler> logger,
            IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }
        public async Task Consume(ConsumeContext<SaveMessageIntegrationEvent> context)
        {
            try
            {
                await _messageService.InsertAsync(new Models.Messages.MessageRequest
                {
                    ChatId = context.Message.ChatId,
                    Content = context.Message.Content,
                    ReplyTo = context.Message.ReplyTo,

                }, context.Message.From, context.Message.CreatedAt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Message saving failed.");
            }
        }
    }
}
