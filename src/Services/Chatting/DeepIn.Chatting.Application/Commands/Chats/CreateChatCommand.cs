using DeepIn.Chatting.Application.Dtos;
using DeepIn.Chatting.Domain.ChatAggregate;
using MediatR;

namespace DeepIn.Chatting.Application.Commands.Chats
{
    public class CreateChatCommand : IRequest<ChatDTO>
    {
        public string Name { get; set; }
        public string AvatarId { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public ChatType Type { get; set; }
        public bool IsPrivate { get; set; }
        public string UserId { get; set; }
    }
}
