using EatMeat.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace AuthCenter.Domain.Entities
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
    }
}
