using DeepIn.Chatting.Domain.ChatAggregate;
using DeepIn.Domain.Exceptions;
using MediatR;

namespace DeepIn.Chatting.Application.Commands.Chats
{
    public class DeleteChatCommandHandler : IRequestHandler<DeleteChatCommand, Unit>
    {
        private readonly IChatRepository _chatRepository;
        public DeleteChatCommandHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<Unit> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.FindById(request.Id);
            if (chat == null || chat.IsDeleted)
            {
                throw new DomainException($"chat was not found, chat id:{request.Id}");
            }
            chat.IsDeleted = true;
            _chatRepository.Update(chat);
            await _chatRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
