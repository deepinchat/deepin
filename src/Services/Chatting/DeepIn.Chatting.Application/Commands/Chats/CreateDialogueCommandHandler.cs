using DeepIn.Chatting.Application.Dtos;
using DeepIn.Chatting.Domain.ChatAggregate;
using MediatR;

namespace DeepIn.Chatting.Application.Commands.Chats
{
    public class CreateDialogueCommandHandler : IRequestHandler<CreateDialogueCommand, ChatDTO>
    {
        private readonly IChatRepository _chatRepository;
        public CreateDialogueCommandHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
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
            await _chatRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return ChatDTO.FromChat(chat);
        }
    }
}
