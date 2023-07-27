using MediatR;

namespace DeepIn.Chatting.Application.Commands.Chats;

public class LeaveChatCommand : IRequest<Unit>
{
    public string Id { get; set; }
    public string UserId { get; set; }
}
