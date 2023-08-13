using DeepIn.Caching;
using DeepIn.Chatting.Application.Queries;
using DeepIn.Chatting.Domain.ChatAggregate;
using DeepIn.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DeepIn.Chatting.Application.ChattingDefaults;

namespace DeepIn.Chatting.Application.Commands.Chats
{
    public class JoinChatCommandHandler : IRequestHandler<JoinChatCommand, Unit>
    {
        private readonly IChatQueries _chatQueries;
        private readonly IChatRepository _chatRepository;
        private readonly ICacheManager _cacheManager;
        public JoinChatCommandHandler(
            IChatQueries chatQueries,
            IChatRepository chatRepository,
            ICacheManager cacheManager)
        {
            _chatQueries = chatQueries;
            _chatRepository = chatRepository;
            _cacheManager = cacheManager;
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
            await _cacheManager.RemoveAsync(CacheKeys.GetChats(request.UserId));
            return Unit.Value;
        }
    }
}
