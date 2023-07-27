using MediatR;

namespace DeepIn.Chatting.Application.Commands.Chats;

public class JoinChatCommand : IRequest<Unit>
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public bool IsBot { get; set; }
}
