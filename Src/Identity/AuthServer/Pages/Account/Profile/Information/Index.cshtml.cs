using AuthServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthServer.Pages.Account.Profile.Information
{
	public class IndexModel : PageModel
	{
		private readonly UserManager<ApplicationUser> userManager;

		public IndexModel(UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
		}
		public ApplicationUser ApplicationUser { get; set; }

		public async Task<IActionResult> OnGet()
		{
			ApplicationUser = await userManager.GetUserAsync(User);

			return Page();
		}
	}
}
