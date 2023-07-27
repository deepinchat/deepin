namespace DeepIn.EventBus.Shared.Events;
public record SendEmailIntegrationEvent : IntegrationEvent
{
    public string Subject { get; set; }
    public string To { get; set; }
    public string CC { get; set; }
    public string Body { get; set; }
    public bool IsBodyHtml { get; set; }
    public string IpAddress { get; set; }
}
