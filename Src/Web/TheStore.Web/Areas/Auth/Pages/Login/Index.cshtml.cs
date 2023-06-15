using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TheStore.Web.Domain.Users;
using TheStore.Web.Helpers;
using TheStore.Web.Services.StatusMessage;

namespace TheStore.Web.Areas.Auth.Pages.Login
{
	[BindProperties]
	public class IndexModel : PageModel
	{
		private readonly IStatusMessageService statusMessageService;
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;

		[Required]
		public string? Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		public bool RememberMe { get; set; }

		public IndexModel(IStatusMessageService statusMessageService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			this.statusMessageService = statusMessageService;
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid == false)
			{
				ModelStateVisualizer.Visualize(statusMessageService, ModelState);
				return Page();
			}

			var user = await userManager.FindByNameAsync(Username);

			if (user == null)
			{
				statusMessageService.AddMessage("User not found", MessageType.Error);
				return Page();
			}

			var result = await signInManager.PasswordSignInAsync(user, Password, RememberMe, true);

			if (result.Succeeded)
			{
				return RedirectToPage("./Index");
			}
			else
			{
				statusMessageService.AddMessage("Invalid username or password", MessageType.Error);
				return Page();
			}
		}
	}
}
