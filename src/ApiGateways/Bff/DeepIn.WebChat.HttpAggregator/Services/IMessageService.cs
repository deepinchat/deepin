using DeepIn.WebChat.HttpAggregator.Dtos;
using DeepIn.WebChat.HttpAggregator.Models;

namespace DeepIn.WebChat.HttpAggregator.Services
{
    public interface IMessageService
    {
        Task<MessageData> SendAsync(PostMessageDTO message);
    }
}