using AuthServer.Models;
using AuthServer.Services.StatusMessages;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace AuthServer.Pages.Account.Register
{
	[SecurityHeaders]
	[AllowAnonymous]
	public class Index : PageModel
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IMapper mapper;
		private readonly StatusMessageService statusMessageService;

		public Index(
			UserManager<ApplicationUser> userManager,
			IMapper mapper,
			StatusMessageService statusMessageService)
		{
			this.userManager = userManager;
			this.mapper = mapper;
			this.statusMessageService = statusMessageService;
		}

		[BindProperty]
		public InputModel Input { get; set; } = new();
		public RegisterOptions Options { get; set; } = new();

		public IActionResult OnGet(string returnUrl)
		{
			Input.ReturnUrl = returnUrl;


			// A flag indicating that this page is an auth page so we don't show navbar and footer
			ViewData["IsAuthOp"] = true;

			return Page();
		}

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid == false)
				return Page();

			var user = mapper.Map<ApplicationUser>(Input);

			user.UserName = GenerateUniqueUsername(user.Email);

			var result = await userManager.CreateAsync(user, Input.Password);

			if (result.Succeeded)
			{
				statusMessageService
					.AddMessage(StatusMessageType.Success, "Message_Register_Successful");

				return RedirectToPage();
			}
			else
			{
				result.Errors.AsStatusMessages(statusMessageService);
			}

			return Page();
		}

		private static string GenerateUniqueUsername(string input)
		{
			byte[] hash = MD5.HashData(Encoding.Default.GetBytes(input));
			return Base64UrlEncoder.Encode(hash);
		}
	}
}