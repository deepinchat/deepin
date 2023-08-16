using DeepIn.WebChat.HttpAggregator.Dtos;

namespace DeepIn.WebChat.HttpAggregator.HttpClients
{
    public interface IUserProfileHttpClient
    {
        Task<UserProfileDTO> GetUserProfileAsync();
        Task<UserProfileDTO> GetUserProfileAsync(string userId);
    }
}