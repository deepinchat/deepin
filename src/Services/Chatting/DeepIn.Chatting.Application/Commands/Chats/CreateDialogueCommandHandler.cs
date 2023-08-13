using DeepIn.Caching;
using DeepIn.Chatting.Application.Dtos;
using DeepIn.Chatting.Domain.ChatAggregate;
using MediatR;
using static DeepIn.Chatting.Application.ChattingDefaults;

namespace DeepIn.Chatting.Application.Commands.Chats
{
    public class CreateDialogueCommandHandler : IRequestHandler<CreateDialogueCommand, ChatDTO>
    {
        private readonly IChatRepository _chatRepository;
        private readonly ICacheManager _cacheManager;
        public CreateDialogueCommandHandler(
            IChatRepository chatRepository,
            ICacheManager cacheManager)
        {
            _chatRepository = chatRepository;
            _cacheManager = cacheManager;
        }
        public async Task<ChatDTO> Handle(CreateDialogueCommand request, CancellationToken cancellationToken)
        {
            var chat = new Chat(ChatType.Dialogue, request.UserId)
            {
                IsPrivate = true,
                AvatarId = null,
                Description = null,
                Link = null,
                Name = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsVerified = false,
                CreatedBy = request.UserId
            };
            chat.AddMember(request.TargetUserId, false, true, request.IsBot);
            _chatRepository.Add(chat);
            await _chatRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            await _cacheManager.RemoveAsync(CacheKeys.GetChats(request.UserId));
            await _cacheManager.RemoveAsync(CacheKeys.GetChats(request.TargetUserId));
            return ChatDTO.FromChat(chat);
        }
    }
}
