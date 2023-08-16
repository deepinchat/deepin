namespace DeepIn.WebChat.HttpAggregator.Dtos
{
    public class PostMessageDTO
    {
        public string ChatId { get; set; }
        public string Content { get; set; }
        public string ReplyTo { get; set; }
    }
}
