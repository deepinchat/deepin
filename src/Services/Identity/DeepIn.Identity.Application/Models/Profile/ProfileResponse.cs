using DeepIn.Identity.Domain.Entities;
using IdentityModel;

namespace DeepIn.Identity.Application.Models.Profile
{
    public class ProfileResponse : ProfileRequest
    {
        public string UserId { get; set; }
        public ProfileResponse() { }
        public ProfileResponse(string id, List<UserClaim> userClaims)
        {
            UserId = id;
            NickName = userClaims.FirstOrDefault(s => s.ClaimType == JwtClaimTypes.NickName)?.ClaimValue;
            Picture = userClaims.FirstOrDefault(s => s.ClaimType == JwtClaimTypes.Picture)?.ClaimValue;
            ZoneInfo = userClaims.FirstOrDefault(s => s.ClaimType == JwtClaimTypes.ZoneInfo)?.ClaimValue;
            Locale = userClaims.FirstOrDefault(s => s.ClaimType == JwtClaimTypes.Locale)?.ClaimValue;
            BirthDate = userClaims.FirstOrDefault(s => s.ClaimType == JwtClaimTypes.BirthDate)?.ClaimValue;
            Gender = userClaims.FirstOrDefault(s => s.ClaimType == JwtClaimTypes.Gender)?.ClaimValue;
            Bio = userClaims.FirstOrDefault(s => s.ClaimType == "bio")?.ClaimValue;
        }
    }
}
