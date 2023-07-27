using MediatR;

namespace DeepIn.Chatting.Application.Commands.Chats;

public class DeleteChatCommand : IRequest<Unit>
{
    public string Id { get; set; }
    public DeleteChatCommand(string id)
    {
        Id = id;
    }
}
