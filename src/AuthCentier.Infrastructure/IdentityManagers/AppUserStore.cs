using AuthCenter.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthCenter.Infrastructure.IdentityManagers
{
    public class AppUserStore : UserStore<User, Role, AuthContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
    {
        public AppUserStore(AuthContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
