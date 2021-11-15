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
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
            };
        }
    }
}
