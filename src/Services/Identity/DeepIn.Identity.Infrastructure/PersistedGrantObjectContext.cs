using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Identity.Infrastructure;

public class PersistedGrantObjectContext : PersistedGrantDbContext<PersistedGrantObjectContext>
{
    public PersistedGrantObjectContext(
        DbContextOptions<PersistedGrantObjectContext> options,
        OperationalStoreOptions storeOptions) : base(options, storeOptions) { }
}
