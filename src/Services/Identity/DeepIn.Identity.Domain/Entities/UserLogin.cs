using DeepIn.Domain;
using Microsoft.AspNetCore.Identity;

namespace DeepIn.Identity.Domain.Entities
{
    public class UserLogin : IdentityUserLogin<string>, IEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}