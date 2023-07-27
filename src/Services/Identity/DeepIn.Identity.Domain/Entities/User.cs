using DeepIn.Domain;
using Microsoft.AspNetCore.Identity;

namespace DeepIn.Identity.Domain.Entities
{
    public class User : IdentityUser, IEntity
    {
        public const string VerifyUserEmailTokenPurpose = "VerifyUserEmailToken";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; set; }
        public static string GenerateGuidUserName() => Guid.NewGuid().ToString("N");
    }
}