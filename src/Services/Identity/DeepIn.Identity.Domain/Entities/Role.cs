using DeepIn.Domain;
using Microsoft.AspNetCore.Identity;

namespace DeepIn.Identity.Domain.Entities
{
    public class Role : IdentityRole, IEntity
    {
        public string DisplayName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}