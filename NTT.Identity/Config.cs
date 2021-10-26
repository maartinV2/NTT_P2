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
                new ApiScope("NTTapi", "NTT.API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "NTTweb",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:4300/assets/oidc-client/signin-callback.html",
                        "http://localhost:4300/assets/oidc-client/silent-callback.html"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:4300/assets/oidc-client/signout-callback.html"
                    },
                    AllowedCorsOrigins = {"http://localhost:4300"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "NTTapi"
                    },
                    RequireConsent = false,
                    AccessTokenLifetime = 3600
                }
            };
    }
}