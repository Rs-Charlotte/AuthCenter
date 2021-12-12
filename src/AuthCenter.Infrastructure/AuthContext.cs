using AuthCenter.Domain.Entities;
using DotNetCore.CAP;
using EatMeat.Infrastructure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthCenter.Infrastructure
{
    public class AuthContext : EFContext
    {
        public AuthContext(DbContextOptions<AuthContext> options, IMediator mediator, ICapPublisher capBus) : base(options, mediator, capBus)
        {
        }
        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<UserClaim> UserClaims { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }
        public override DbSet<UserLogin> UserLogins { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
