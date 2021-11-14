using Microsoft.AspNetCore.Identity;

namespace AuthCenter.Domain.Entities
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public UserLogin() { }
    }
}
