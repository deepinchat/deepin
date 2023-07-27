using DeepIn.Domain;
using Microsoft.AspNetCore.Identity;

namespace DeepIn.Identity.Domain.Entities
{
    public class UserToken : IdentityUserToken<string>, IEntity
    {
    }
}