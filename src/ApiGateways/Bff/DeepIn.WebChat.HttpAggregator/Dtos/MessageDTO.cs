namespace DeepIn.WebChat.HttpAggregator.Dtos
{
    public class MessageDTO : PostMessageDTO
    {
        public string Id { get; set; }
        public string From { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
