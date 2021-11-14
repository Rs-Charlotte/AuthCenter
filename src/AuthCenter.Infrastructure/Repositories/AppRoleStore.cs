using AuthCenter.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthCenter.Infrastructure.Repositories
{
    public class AppRoleStore : RoleStore<Role, AuthContext, Guid, UserRole, RoleClaim>
    {
        public AppRoleStore(AuthContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
