using DeepIn.WebChat.HttpAggregator.Dtos;

namespace DeepIn.WebChat.HttpAggregator.HttpClients
{
    public interface IMessageHttpClient
    {
        Task<MessageDTO> SendAysnc(PostMessageDTO message);
    }
}