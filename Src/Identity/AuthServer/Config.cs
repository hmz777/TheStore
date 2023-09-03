using Duende.IdentityServer.Models;

namespace AuthServer
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> IdentityResources =>
			new IdentityResource[]
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
			};

		public static IEnumerable<ApiScope> ApiScopes =>
			new ApiScope[]
			{
				new ApiScope("Scope1")
			};

		public static IEnumerable<ApiResource> ApiResources =>
			new ApiResource[]
			{
				new ApiResource("ApiResource1")
			};

		public static IEnumerable<Client> Clients =>
			new Client[]
			{
				// Interactive client using code flow + pkce
				new Client
				{
					ClientId = "TheStore.Web.Blazor",
					ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
					AllowedGrantTypes = GrantTypes.Code,
					AllowedCorsOrigins = { "https://localhost:7074" },
					RedirectUris = { "https://localhost:7074/signin-oidc", "https://localhost:7074/authentication/login-callback"  },
					FrontChannelLogoutUri = "https://localhost:7074/signout-oidc",
					PostLogoutRedirectUris = { "https://localhost:7074/signout-callback-oidc" },
					AllowedScopes = { "openid", "profile", "Scope1", "ApiResource1" }
				}
			};
	}
}