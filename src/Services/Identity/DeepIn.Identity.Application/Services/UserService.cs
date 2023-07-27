using DeepIn.Caching;
using DeepIn.Identity.Application.Models.Profile;
using DeepIn.Identity.Domain.Entities;
using DeepIn.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Identity.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IdentityObjectContext _dbContext;
        public UserService(
            ICacheManager cacheManager,
            IdentityObjectContext dbContext)
        {
            _cacheManager = cacheManager;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _cacheManager.GetOrCreateAsync(CacheKeyConstants.GetAllUsers, () => _dbContext.Users.ToListAsync());
        }
        public async Task<IEnumerable<UserClaim>> GetUserClaims(string[] userIds = null)
        {
            var list = await _cacheManager.GetOrCreateAsync(CacheKeyConstants.GetAllProfiles, () => _dbContext.UserClaims.ToListAsync());
            if (userIds != null && userIds.Any())
            {
                list = list.Where(x => userIds.Contains(x.UserId)).ToList();
            }
            return list;
        }
        public async Task<User> GetUserByNameOrEmail(string nameOrEmail)
        {
            var users = await this.GetUsers();
            return users.FirstOrDefault(x => x.UserName.Equals(nameOrEmail, StringComparison.OrdinalIgnoreCase) || x.Email.Equals(nameOrEmail, StringComparison.OrdinalIgnoreCase));
        }
        private async Task<ProfileResponse> LoadProfileByUserId(string userId)
        {
            var claims = await _dbContext.UserClaims.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
            return new ProfileResponse(userId, claims);
        }
        public async Task<ProfileResponse> GetProfileByUserId(string userId)
        {
            return await _cacheManager.GetOrCreateAsync(CacheKeyConstants.GetProfileById(userId), () => this.LoadProfileByUserId(userId));
        }
        public async Task<ProfileResponse> UpdateProfile(string userId, ProfileRequest request)
        {
            var claims = await _dbContext.UserClaims.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
            if (claims.Any())
            {
                _dbContext.RemoveRange(claims);
            }
            claims = request.ToClaims(userId).Where(x => !string.IsNullOrEmpty(x.ClaimValue)).ToList();
            await _dbContext.AddRangeAsync(claims);
            await _dbContext.SaveChangesAsync();
            await _cacheManager.RemoveAsync(CacheKeyConstants.GetProfileById(userId));
            return new ProfileResponse(userId, claims);
        }

        public async Task<ProfileResponse> GetProfileByUserNameOrEmail(string search)
        {
            var user = await this.GetUserByNameOrEmail(search);
            if (user == null) return null;
            return await this.GetProfileByUserId(user.Id);
        }
    }
}
