using DeepIn.Domain;

namespace DeepIn.Emailing.API.Domain.Entities
{
    public class MailObject : Entity
    {
        public string From { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string IpAddress { get; set; }
        public bool IsBodyHtml { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}


