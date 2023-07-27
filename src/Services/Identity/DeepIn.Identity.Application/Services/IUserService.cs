using DeepIn.Identity.Application.Models.Profile;
using DeepIn.Identity.Domain.Entities;

namespace DeepIn.Identity.Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserClaim>> GetUserClaims(string[] userIds = null);
        Task<ProfileResponse> GetProfileByUserId(string userId);
        Task<ProfileResponse> GetProfileByUserNameOrEmail(string search);
        Task<ProfileResponse> UpdateProfile(string userId, ProfileRequest request);
    }
}