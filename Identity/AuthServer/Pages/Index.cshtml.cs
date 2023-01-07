using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace TheStore.Auth.Pages
{
	[AllowAnonymous]
	public class Index : PageModel
	{
		public string Version;

		public void OnGet()
		{
			Version = typeof(Duende.IdentityServer.Hosting.IdentityServerMiddleware).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+').First();
		}
	}
}