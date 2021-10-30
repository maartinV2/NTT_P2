using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace NTT.Identity
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("ntt.api", "NTT API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client {
                    RequireConsent = false,
                    ClientId = "angular_spa",
                    ClientName = "Angular SPA",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile", "email", "ntt.api" },
                    RedirectUris = {"http://localhost:4300/auth-callback"},
                    PostLogoutRedirectUris = {"http://localhost:4300/"},
                    AllowedCorsOrigins = {"http://localhost:4200"},
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600
                }
            };
    }
}