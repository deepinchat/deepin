namespace DeepIn.Messaging.API.Domain
{
    public class Message : IDocument
    {
        public string Id { get; set; }
        public string ChatId { get; set; }
        public string From { get; set; }
        public string Content { get; set; }
        public string ReplyTo { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
