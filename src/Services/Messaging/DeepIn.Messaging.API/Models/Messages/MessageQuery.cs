using DeepIn.Application.Models;

namespace DeepIn.Messaging.API.Models.Messages;

public class MessageQuery : PagedQuery
{
    public string Keywords { get; set; }
    public string ChatId { get; set; }
    public string From { get; set; }
}
