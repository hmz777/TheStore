using Duende.IdentityServer.Models;

namespace AuthServer
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> IdentityResources =>
			new IdentityResource[]
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile()
				{
					UserClaims = { "name", "first name", "last name", "email" }
				}
			};

		public static IEnumerable<ApiScope> ApiScopes =>
			Array.Empty<ApiScope>();

		public static IEnumerable<ApiResource> ApiResources =>
			Array.Empty<ApiResource>();

		public static IEnumerable<Client> Clients =>
			new Client[]
			{
				// Interactive client using code flow + PKCE
				new Client
				{
					ClientName = "TheStore SPA",
					ClientId = "TheStore.Web.Blazor",
					ClientUri = "https://localhost:7074",
					LogoUri = "https://localhost:7074/media/system/logo.png",
					ClientSecrets = { new Secret("secret".Sha256()) },
					AllowedGrantTypes = GrantTypes.Code,
					AllowedCorsOrigins = { "https://localhost:7074" },
					RedirectUris = {
						"https://localhost:7074/signin-oidc",
						"https://localhost:7074/authentication/login-callback",
					},
					FrontChannelLogoutUri = "https://localhost:7074/signout-oidc",
					PostLogoutRedirectUris = {
						"https://localhost:7074/signout-callback-oidc",
						"https://localhost:7074/authentication/logout-callback",
					},
					AllowedScopes = { "openid", "profile" },
					RequireClientSecret = false
				}
			};
	}
}