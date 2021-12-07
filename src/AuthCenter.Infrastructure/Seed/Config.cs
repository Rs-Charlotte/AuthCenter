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
                new ApiScope("is4admin"),
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
                new Client()
                {
                    ClientId = "IdentityServer4-Admin",
                    ClientName = "IdentityServer4-后台",
                    //ClientSecrets = { new Secret("zzxfkme".Sha256()) },

                    // 授权码模式
                    AllowedGrantTypes = GrantTypes.Code,
                    // 基于授权代码的令牌是否需要验证密钥,默认为false
                    RequirePkce = true,
                    // 令牌端点请求令牌时不需要客户端密钥
                    RequireClientSecret = false,

                    RedirectUris =           { "http://localhost:4200/index.html", "http://localhost:4200/silent-refresh.html" },
                    PostLogoutRedirectUris = { "http://localhost:4200/index.html" },

                    AllowedCorsOrigins = {"http://localhost:4200"},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "is4admin"
                    }
                }
            };
        }
    }
}
