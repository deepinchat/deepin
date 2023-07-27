using DeepIn.Messaging.API.Domain;

namespace DeepIn.Messaging.API.Models.Messages
{
    public class MessageResponse : MessageRequest
    {
        public string Id { get; set; }
        public string From { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public MessageResponse() { }
        public MessageResponse(Message message)
        {
            this.Id = message.Id;
            this.From = message.From;
            this.CreatedAt = message.CreatedAt;
            this.ModifiedAt = message.ModifiedAt;
            this.Content = message.Content;
            this.ChatId = message.ChatId;
            this.ReplyTo = message.ReplyTo;
        }
    }
}
