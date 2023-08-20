namespace DeepIn.WebChat.HttpAggregator.Dtos
{
    public class MessageDTO : PostMessageDTO
    {
        public string Id { get; set; }
        public string From { get; set; }
        public long CreatedAt { get; set; }
        public long ModifiedAt { get; set; }
    }
}
