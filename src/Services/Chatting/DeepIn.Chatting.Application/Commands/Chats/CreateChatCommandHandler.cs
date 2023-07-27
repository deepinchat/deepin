using DeepIn.Chatting.Application.Commands.Chats;
using DeepIn.Chatting.Application.Dtos;
using DeepIn.Chatting.Domain.ChatAggregate;
using MediatR;

namespace DeepIn.Chatting.Application.Commands
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, ChatDTO>
    {
        private readonly IChatRepository _chatRepository;
        public CreateChatCommandHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
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
            await _chatRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return ChatDTO.FromChat(chat);
        }
    }
}
