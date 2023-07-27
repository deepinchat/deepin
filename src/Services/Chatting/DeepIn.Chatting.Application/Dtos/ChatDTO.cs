using DeepIn.Chatting.Domain.ChatAggregate;

namespace DeepIn.Chatting.Application.Dtos
{
    public class ChatDTO
    {
        public string Id { get; set; }
        public ChatType Type { get; private set; }
        public string Name { get; set; }
        public string AvatarId { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsPrivate { get; set; }
        public bool IsVerified { get; set; }
        public IEnumerable<ChatMemberDTO> ChatMembers { get; set; } = new List<ChatMemberDTO>();

        public static ChatDTO FromChat(Chat chat)
        {
            var chatDTO = new ChatDTO()
            {
                Id = chat.Id,
                AvatarId = chat.AvatarId,
                CreatedAt = chat.CreatedAt,
                UpdatedAt = chat.UpdatedAt,
                IsPrivate = chat.IsPrivate,
                IsVerified = chat.IsVerified,
                Description = chat.Description,
                Link = chat.Link,
                Name = chat.Name,
                Type = chat.Type
            };
            if (chat.ChatMembers.Any())
            {
                chatDTO.ChatMembers = chat.ChatMembers.Select(s => ChatMemberDTO.FromChatMember(s));
            }
            return chatDTO;
        }
    }
}
