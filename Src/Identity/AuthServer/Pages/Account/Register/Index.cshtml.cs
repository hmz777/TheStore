using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthServer.Pages.Account.Register
{
	[SecurityHeaders]
	[AllowAnonymous]
	public class Index : PageModel
	{
		[BindProperty]
		public InputModel Input { get; set; } = new();
		public RegisterOptions Options { get; set; } = new();

		public IActionResult OnGet()
		{
			return Page();
		}
	}
}
