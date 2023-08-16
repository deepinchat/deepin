using DeepIn.WebChat.HttpAggregator.Dtos;
using DeepIn.WebChat.HttpAggregator.HttpClients;
using DeepIn.WebChat.HttpAggregator.Models;

namespace DeepIn.WebChat.HttpAggregator.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageHttpClient _messageHttpClient;
        private readonly IUserProfileHttpClient _userProfileHttpClient;
        public MessageService(
            IMessageHttpClient messageHttpClient,
            IUserProfileHttpClient userProfileHttpClient)
        {
            _messageHttpClient = messageHttpClient;
            _userProfileHttpClient = userProfileHttpClient;
        }
        public async Task<MessageData> SendAsync(PostMessageDTO message)
        {
            var messageResponse = await _messageHttpClient.SendAysnc(message);
            if (messageResponse == null)
                return null;
            var userResponse = await _userProfileHttpClient.GetUserProfileAsync(messageResponse.From);
            return new MessageData(messageResponse, userResponse);
        }
    }
}
