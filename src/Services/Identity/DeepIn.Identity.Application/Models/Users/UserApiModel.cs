namespace DeepIn.Identity.Application.Models.Users
{
    public class UserApiModel
    {
        public  string Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; set; }
        public IEnumerable<UserClaimApiModel> UserClaims { get; set; }
    }
}
