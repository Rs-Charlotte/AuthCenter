using AuthCenter.Infrastructure.Repositories;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace AuthCenter.IdentityServer4.Extensions
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly AppUserManager _userManager;

        public ResourceOwnerPasswordValidator(AppUserManager userManager)
        {
            _userManager = userManager;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _userManager.FindByNameAsync(context.UserName).Result;
            if (user != null)
            {
                var result = _userManager.CheckPasswordAsync(user, context.Password).Result;
                if (result)
                {
                    context.Result = new GrantValidationResult(
                        subject: context.UserName, 
                        authenticationMethod: OidcConstants.AuthenticationMethods.Password);
                }
                else
                {
                    //验证失败
                    context.Result = new GrantValidationResult(
                        TokenRequestErrors.InvalidGrant,
                        "invalid custom credential"
                        );
                }
            }
            else
            {
                //验证失败
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    "invalid custom credential"
                    );
            }

            return Task.FromResult(0);
        }
    }
}
