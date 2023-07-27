using DeepIn.Caching;
using DeepIn.Chatting.Domain.Events;
using MediatR;
using static DeepIn.Chatting.Application.ChattingDefaults;

namespace DeepIn.Chatting.Application.DomainEventHandlers
{
    public class ChatChangedDomainEventHandler : INotificationHandler<ChatChangedDomainEvent>
    {
        private readonly ICacheManager _cacheManager;
        public ChatChangedDomainEventHandler(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        public async Task Handle(ChatChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            foreach(var userId in notification.UserIds)
            {
                await _cacheManager.RemoveAsync(CacheKeys.GetChats(userId));
            }
        }
    }
}
