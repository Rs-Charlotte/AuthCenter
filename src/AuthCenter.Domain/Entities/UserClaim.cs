using Microsoft.AspNetCore.Identity;

namespace AuthCenter.Domain.Entities
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public UserClaim() { }
    }
}
