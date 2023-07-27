namespace DeepIn.WebSPA.HttpAggregator.Models
{
    public class SendMessageRequest
    {
        public string ChatId { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public string ReplyTo { get; set; }
    }
}
