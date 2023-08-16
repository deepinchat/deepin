using DeepIn.WebChat.HttpAggregator.Config;
using DeepIn.WebChat.HttpAggregator.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DeepIn.WebChat.HttpAggregator.HttpClients
{
    public class MessageHttpClient : IMessageHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _urls;

        public MessageHttpClient(HttpClient httpClient, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _urls = config.Value;
        }

        public async Task<MessageDTO> SendAysnc(PostMessageDTO message)
        {
            var url = $"{_urls.Messaging}{UrlsConfig.MessagingAPI.PostMessage()}";
            var content = new StringContent(JsonConvert.SerializeObject(message), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var messageResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MessageDTO>(messageResponse);
        }
    }
}
