using Microsoft.AspNetCore.Identity;

namespace AuthCenter.Domain.Entities
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public RoleClaim() { }
    }
}