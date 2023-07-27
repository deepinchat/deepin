using DeepIn.EventBus;

namespace DeepIn.Emailing.API.Application.IntegrationEvents.Events;

public record SendEmailIntegrationEvent : IntegrationEvent
{
    public string Subject { get; set; }
    public string To { get; set; }
    public string Body { get; set; }
    public bool IsBodyHtml { get; set; }
    public string IpAddress { get; set; }
}
