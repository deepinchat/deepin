using DeepIn.Chatting.Domain.ChatAggregate;

namespace DeepIn.Chatting.Application.Dtos
{
    public class ChatMemberDTO
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Alias { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
        public bool IsBot { get; set; }
        public DateTime CreatedAt { get; set; }
        public static ChatMemberDTO FromChatMember(ChatMember chatMember)
        {
            return new ChatMemberDTO
            {
                Alias = chatMember.Alias,
                CreatedAt = chatMember.CreatedAt,
                UserId = chatMember.UserId,
                Id = chatMember.Id,
                IsAdmin = chatMember.IsAdmin,
                IsOwner = chatMember.IsOwner,
                IsBot = chatMember.IsBot
            };
        }
    }
}
