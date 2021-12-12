using Microsoft.AspNetCore.Identity;

namespace AuthCenter.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
            Id = Guid.NewGuid();
            CreateTime = DateTimeOffset.UtcNow;
            UpdateTime = CreateTime;
        }

        public Role(string roleName)
        {
            Id = Guid.NewGuid();
            Name = roleName;
            CreateTime = DateTimeOffset.UtcNow;
            UpdateTime = CreateTime;
        }

        public override Guid Id { get; set; }
        public override string Name { get; set; } 
        public override string NormalizedName { get; set; }
        public override string ConcurrencyStamp { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }
}
