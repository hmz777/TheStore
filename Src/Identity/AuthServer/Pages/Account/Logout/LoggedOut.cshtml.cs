using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthServer.Pages.Logout
{
	[SecurityHeaders]
	[AllowAnonymous]
	public class LoggedOut : PageModel
	{
		private readonly IIdentityServerInteractionService _interactionService;

		public LoggedOutViewModel View { get; set; }

		public LoggedOut(IIdentityServerInteractionService interactionService)
		{
			_interactionService = interactionService;
		}

		public async Task<IActionResult> OnGet(string logoutId)
		{
			// get context information (client name, post logout redirect URI and iframe for federated signout)
			var logout = await _interactionService.GetLogoutContextAsync(logoutId);

			return Redirect(logout.PostLogoutRedirectUri ?? "~/");
		}
	}
}