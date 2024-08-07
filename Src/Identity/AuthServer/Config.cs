using Duende.IdentityServer.Models;
using IdentityModel;

namespace AuthServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            [
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(){
                    UserClaims =
                    [
                        JwtClaimTypes.PreferredUserName,
                        JwtClaimTypes.Email,
                        "first_name",
                        "last_name",
                        JwtClaimTypes.PhoneNumber,
                        JwtClaimTypes.BirthDate,
                    ]
                }
            ];

        public static IEnumerable<ApiScope> ApiScopes => [
            new ApiScope("Api.Catalog.User.Read", "Read from Catalog API as User"),
            new ApiScope("Api.Cart.User.Read", "Read from Cart API as User"),
            new ApiScope("Api.Cart.User.Write", "Write to Cart API as User"),
        ];

        public static IEnumerable<ApiResource> ApiResources => [];

        // TODO: Reconfigure later for refresh tokens and dynamic scopes and permissions
        public static IEnumerable<Client> Clients =>
            [
                // Interactive client using code flow + PKCE
                new Client() {
                    ClientName = "TheStore SPA",
                    ClientId = "TheStore.Web.Blazor",
                    ClientUri = "https://localhost:7676",
                    LogoUri = "https://localhost:7676/media/system/logo.png",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedCorsOrigins = { "https://localhost:7676" },
                    RedirectUris = {
                        "https://localhost:7676/signin-oidc"
                    },
                    FrontChannelLogoutUri = "https://localhost:7676/signout-oidc",
                    PostLogoutRedirectUris = {
                        "https://localhost:7676/signout-callback-oidc"
                    },
                    AllowedScopes = { "openid", "profile", "Api.Catalog.User.Read", "Api.Cart.User.Read", "Api.Cart.User.Write" },
                    RequireConsent = false,
                    AllowOfflineAccess = false,
                    AlwaysIncludeUserClaimsInIdToken = true
                }
            ];
    }
}