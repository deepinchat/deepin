namespace DeepIn.Chatting.Application.Dtos;

public record MessageDTO
{
    public string Id { get; set; }
    public string ChatId { get; set; }
    public string From { get; set; }
    public string Content { get; set; }
    public string ReplyTo { get; set; }
    public DateTime CreatedAt { get; set; }
}
