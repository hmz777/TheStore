using AuthServer.Models;
using AuthServer.Services.StatusMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace AuthServer.Pages.Account.ConfirmAccountEmail
{
	[SecurityHeaders]
	[AllowAnonymous]
	public class IndexModel : PageModel
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly StatusMessageService statusMessageService;

		public IndexModel(
			UserManager<ApplicationUser> userManager,
			StatusMessageService statusMessageService)
		{
			this.userManager = userManager;
			this.statusMessageService = statusMessageService;
		}

		[FromQuery]
		public InputModel Input { get; set; }

		public async Task<IActionResult> OnGet()
		{
			var user = await userManager.FindByIdAsync(Input.UserId);

			if (user == null)
			{
				statusMessageService.AddMessage(StatusMessageType.Error, "Message_Error_UserNotFound");
			}
			else
			{
				var result = await userManager
					.ConfirmEmailAsync(user, Encoding.Default.GetString(WebEncoders.Base64UrlDecode(Input.Token)));

				if (result.Succeeded)
				{
					statusMessageService.AddMessage(StatusMessageType.Success, "Message_Success_AccountConfirmed");
				}
				else
				{
					result.Errors.AsStatusMessages(statusMessageService);
				}
			}

			return RedirectToPage("/Account/Login/Index", new { returnUrl = Input.ReturnUrl });
		}
	}
}