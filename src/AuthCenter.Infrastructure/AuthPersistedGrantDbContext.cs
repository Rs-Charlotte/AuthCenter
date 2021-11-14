using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace AuthCenter.Infrastructure
{
    public class AuthPersistedGrantDbContext : PersistedGrantDbContext<AuthPersistedGrantDbContext>
    {
        public AuthPersistedGrantDbContext(DbContextOptions options, OperationalStoreOptions storeOptions) : base(options, storeOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
