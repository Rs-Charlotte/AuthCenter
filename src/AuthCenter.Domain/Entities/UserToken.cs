using Microsoft.AspNetCore.Identity;

namespace AuthCenter.Domain.Entities
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public UserToken() { }
    }
}
