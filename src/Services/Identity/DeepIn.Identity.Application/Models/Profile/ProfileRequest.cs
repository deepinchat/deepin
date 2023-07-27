using DeepIn.Identity.Domain.Entities;
using IdentityModel;

namespace DeepIn.Identity.Application.Models.Profile
{
    public class ProfileRequest
    {
        public string NickName { get; set; }
        public string Picture { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string Bio { get; set; }
        public string ZoneInfo { get; set; }
        public string Locale { get; set; }
        public List<UserClaim> ToClaims(string userId)
        {
            return new List<UserClaim>
            {
                new UserClaim()
                {
                     ClaimType = "bio",
                     ClaimValue= Bio,
                     UserId= userId
                },
                new UserClaim()
                {
                     ClaimType = JwtClaimTypes.NickName,
                     ClaimValue= NickName,
                     UserId= userId
                },
                new UserClaim()
                {
                     ClaimType = JwtClaimTypes.BirthDate,
                     ClaimValue= BirthDate,
                     UserId= userId
                },
                new UserClaim()
                {
                     ClaimType = JwtClaimTypes.Gender,
                     ClaimValue= Gender,
                     UserId= userId
                },
                new UserClaim()
                {
                     ClaimType = JwtClaimTypes.Picture,
                     ClaimValue= Picture,
                     UserId= userId
                },
                new UserClaim()
                {
                     ClaimType = JwtClaimTypes.ZoneInfo,
                     ClaimValue= ZoneInfo,
                     UserId= userId
                },
                new UserClaim()
                {
                     ClaimType = JwtClaimTypes.Locale,
                     ClaimValue= Locale,
                     UserId= userId
                }
            };
        }
    }
}