namespace DeepIn.EventBus.Shared.Events;

public record PushMessageIntegrationEvent : IntegrationEvent
{
    public string MessageId { get; set; }
    public string ChatId { get; set; }
    public string Content { get; set; }
    public string ReplyTo { get; set; }
    public string From { get; set; }
    public DateTime CreatedAt { get; set; }
}
