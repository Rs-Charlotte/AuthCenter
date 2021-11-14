using Microsoft.AspNetCore.Identity;

namespace AuthCenter.Domain.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public UserRole() { }
    }
}
