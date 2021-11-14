using AuthCenter.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthCenter.Infrastructure.IdentityManagers
{
    public class AppRoleStore : RoleStore<Role, DbContext, Guid, UserRole, RoleClaim>
    {
        public AppRoleStore(AuthContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
