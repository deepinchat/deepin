namespace DeepIn.EventBus.Shared.Events
{
    public record SaveMessageIntegrationEvent : IntegrationEvent
    {
        public string ChatId { get; set; }
        public string Content { get; set; }
        public string ReplyTo { get; set; }
        public string From { get; set; }
        public DateTime CreatedAt { get; set; }
        public SaveMessageIntegrationEvent(
            string chatId,
            string content,
            string replyTo,
            string from,
            DateTime createdAt
            )
        {
            ChatId = chatId;
            Content = content;
            ReplyTo = replyTo;
            From = from;
            CreatedAt = createdAt;
        }
    }
}
