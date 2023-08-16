using DeepIn.WebChat.HttpAggregator.Dtos;

namespace DeepIn.WebChat.HttpAggregator.Models
{
    public class MessageData : MessageDTO
    {
        public UserProfileDTO User { get; set; }
        public MessageData() { }
        public MessageData(MessageDTO message, UserProfileDTO user)
        {
            this.User = user;
            this.Content = message.Content;
            this.From = message.From;
            this.ReplyTo = message.ReplyTo;
            this.ChatId = message.ChatId;
            this.CreatedAt = message.CreatedAt;
            this.ModifiedAt = message.ModifiedAt;
        }
    }
}
