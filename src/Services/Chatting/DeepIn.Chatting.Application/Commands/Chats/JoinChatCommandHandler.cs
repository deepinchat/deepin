using DeepIn.Chatting.Application.Queries;
using DeepIn.Chatting.Domain.ChatAggregate;
using DeepIn.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepIn.Chatting.Application.Commands.Chats
{
    public class JoinChatCommandHandler : IRequestHandler<JoinChatCommand, Unit>
    {
        private readonly IChatQueries _chatQueries;
        private readonly IChatRepository _chatRepository;
        public JoinChatCommandHandler(
            IChatQueries chatQueries,
            IChatRepository chatRepository)
        {
            _chatQueries = chatQueries;
            _chatRepository = chatRepository;
        }

        public async Task<Unit> Handle(JoinChatCommand request, CancellationToken cancellationToken)
        {
            var isUserAlreadyJoined = await _chatQueries.IsUserInChat(request.UserId, request.Id);
            if (isUserAlreadyJoined)
            {
                return Unit.Value;
            }
            var chat = await _chatRepository.FindById(request.Id);
            if (chat == null || chat.IsDeleted)
            {
                throw new DomainException($"chat was not found, chat id:{request.Id}");
            } 
            chat.AddMember(userId: request.UserId, isBot: request.IsBot);
            _chatRepository.Update(chat);
            await _chatRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
