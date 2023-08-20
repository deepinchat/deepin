using DeepIn.WebChat.HttpAggregator.Config;
using DeepIn.WebChat.HttpAggregator.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DeepIn.WebChat.HttpAggregator.HttpClients
{
    public class UserProfileHttpClient : IUserProfileHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _urls;

        public UserProfileHttpClient(HttpClient httpClient, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _urls = config.Value;
        }
        public async Task<UserProfileDTO> GetUserProfileAsync()
        {
            var url = $"{_urls.Identity}{UrlsConfig.IdentityAPI.GetCurrentUserProfile()}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var userResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserProfileDTO>(userResponse);
        }
        public async Task<UserProfileDTO> GetUserProfileAsync(string userId)
        {
            var url = $"{_urls.Identity}{UrlsConfig.IdentityAPI.GetUserProfileById(userId)}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var userResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserProfileDTO>(userResponse);
        }
    }
}
