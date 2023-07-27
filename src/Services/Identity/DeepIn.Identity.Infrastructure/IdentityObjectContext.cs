using DeepIn.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Identity.Infrastructure;
public class IdentityObjectContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public const string DEFAULT_SCHEMA = "identity";
    public IdentityObjectContext(DbContextOptions<IdentityObjectContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureIdentityContext(builder);
    }

    private static void ConfigureIdentityContext(ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("user", DEFAULT_SCHEMA);
        builder.Entity<UserLogin>().ToTable("user_login", DEFAULT_SCHEMA);
        builder.Entity<UserClaim>().ToTable("user_claim", DEFAULT_SCHEMA);
        builder.Entity<UserToken>().ToTable("user_token", DEFAULT_SCHEMA);

        builder.Entity<Role>().ToTable("role", DEFAULT_SCHEMA);
        builder.Entity<RoleClaim>().ToTable("role_claim", DEFAULT_SCHEMA);
        builder.Entity<UserRole>().ToTable("user_role", DEFAULT_SCHEMA);
    }
}
