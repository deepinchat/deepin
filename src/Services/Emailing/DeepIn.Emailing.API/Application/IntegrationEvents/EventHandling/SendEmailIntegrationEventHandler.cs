using DeepIn.Emailing.API.Application.Services;
using DeepIn.Emailing.API.Domain.Entities;
using DeepIn.Emailing.API.Infrastructure;
using DeepIn.EventBus;
using DeepIn.EventBus.Shared.Events;
using MassTransit;

namespace DeepIn.Emailing.API.Application.IntegrationEvents.EventHandling
{
    public class SendEmailIntegrationEventHandler : IIntegrationEventHandler<SendEmailIntegrationEvent>
    {
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;
        private readonly EmailingDbContext _dbContext;
        public SendEmailIntegrationEventHandler(
            ILogger<SendEmailIntegrationEventHandler> logger,
            IEmailSender emailSender,
            EmailingDbContext dbContext)
        {
            _emailSender = emailSender;
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<SendEmailIntegrationEvent> context)
        {
            try
            {
                var @event = context.Message;
                var mail = new MailObject
                {
                    Body = @event.Body,
                    Subject = @event.Subject,
                    To = @event.To,
                    CC = @event.CC,
                    CreatedAt = @event.CreatedAt,
                    IsBodyHtml = @event.IsBodyHtml,
                    IpAddress = @event.IpAddress,
                };
                await _emailSender.SendAsync(@event.To, @event.Subject, @event.Body, @event.IsBodyHtml); ;
                await _dbContext.AddAsync(mail);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Email sending failed.");
            }
        }
    }
}
