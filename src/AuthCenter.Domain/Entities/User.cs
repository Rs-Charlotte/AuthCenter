using Microsoft.AspNetCore.Identity;

namespace AuthCenter.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            Id = Guid.NewGuid();
            CreateTime = DateTimeOffset.UtcNow;
            UpdateTime = CreateTime;
            IsDeleted = false;
        }

        public override Guid Id { get; set; }
        public override string UserName { get; set; }
        public override string NormalizedUserName { get; set; }
        public override string Email { get; set; }
        public override string NormalizedEmail { get; set; }
        public override bool EmailConfirmed { get; set; } = false;
        public override string PhoneNumber { get; set; }
        public override bool PhoneNumberConfirmed { get; set; }
        public override string PasswordHash { get; set; }
        public override string SecurityStamp { get; set; }
        public override string ConcurrencyStamp { get; set; }
        public override bool TwoFactorEnabled { get; set; }
        public override int AccessFailedCount { get; set; }
        public override bool LockoutEnabled { get; set; }
        public override DateTimeOffset? LockoutEnd { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }
}
