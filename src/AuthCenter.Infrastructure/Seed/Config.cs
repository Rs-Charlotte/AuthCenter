using IdentityServer4;
using IdentityServer4.Models;

namespace AuthCenter.Infrastructure.Seed
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("FBank"),
                new ApiScope("StaticResourceServer")
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("User")
                {
                    Scopes = { "FBank", "StaticResourceServer" }
                }
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Login",
                    ClientName = "Login SPA Client",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    RedirectUris =           { "https://localhost:4200/callback.html" },
                    PostLogoutRedirectUris = { "https://localhost:4200/index.html" },
                    AllowedCorsOrigins =     { "https://localhost:4200" },

                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "FBank", 
                        "StaticResourceServer" 
                    }
                },
            };
        }
    }
}
