using DeepIn.Chatting.Application.Queries;
using DeepIn.Chatting.Domain.ChatAggregate;
using DeepIn.Chatting.Infrastructure;
using DeepIn.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Chatting.Application.Commands.Chats;

public class LeaveChatCommandHandler : IRequestHandler<JoinChatCommand, Unit>
{
    private readonly IChatQueries _chatQueries;
    private readonly IChatRepository _chatRepository;
    private readonly ChattingDbContext _dbContext;
    public LeaveChatCommandHandler(
        IChatQueries chatQueries,
        IChatRepository chatRepository,
        ChattingDbContext dbContext)
    {
        _chatQueries = chatQueries;
        _chatRepository = chatRepository;
        _dbContext = dbContext;
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

        await _dbContext.Entry(chat).Collection(x => x.ChatMembers).Query().Where(x => x.UserId == request.UserId).LoadAsync();
        chat.RemoveMember(userId: request.UserId);
        _chatRepository.Update(chat);
        await _chatRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return Unit.Value;
    }
}
