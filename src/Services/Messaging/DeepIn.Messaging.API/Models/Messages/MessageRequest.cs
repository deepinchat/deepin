namespace DeepIn.Messaging.API.Models.Messages;

public class MessageRequest
{
    public string ChatId { get; set; }
    public string Content { get; set; }
    public string ReplyTo { get; set; }
}
