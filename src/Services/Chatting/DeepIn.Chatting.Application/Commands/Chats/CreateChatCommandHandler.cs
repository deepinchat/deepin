using DeepIn.Caching;
using DeepIn.Chatting.Application.Commands.Chats;
using DeepIn.Chatting.Application.Dtos;
using DeepIn.Chatting.Domain.ChatAggregate;
using MediatR;
using static DeepIn.Chatting.Application.ChattingDefaults;

namespace DeepIn.Chatting.Application.Commands
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, ChatDTO>
    {
        private readonly IChatRepository _chatRepository;
        private readonly ICacheManager _cacheManager;
        public CreateChatCommandHandler(
            IChatRepository chatRepository,
            ICacheManager cacheManager)
        {
            _chatRepository = chatRepository;
            _cacheManager = cacheManager;
        }
        public async Task<ChatDTO> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = new Chat(request.Type, request.UserId)
            {
                AvatarId = request.AvatarId,
                Description = request.Description,
                IsPrivate = request.IsPrivate,
                Link = request.Link,
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsVerified = false,
                CreatedBy = request.UserId
            };
            _chatRepository.Add(chat);
            await _chatRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            await _cacheManager.RemoveAsync(CacheKeys.GetChats(request.UserId));
            return ChatDTO.FromChat(chat);
        }
    }
}
