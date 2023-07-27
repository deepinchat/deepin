using DeepIn.Chatting.Application.Dtos;
using MediatR;

namespace DeepIn.Chatting.Application.Commands.Chats
{
    public class CreateDialogueCommand : IRequest<ChatDTO>
    {
        public bool IsBot { get; set; }
        public string TargetUserId { get; set; }
        public string UserId { get; set; }
    }
}
