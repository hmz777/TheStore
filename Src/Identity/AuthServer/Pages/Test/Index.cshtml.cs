using AuthServer.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace AuthServer.Pages.Test
{
	public class IndexModel : PageModel
	{
		private readonly IStringLocalizer<LocalizationResources> stringLocalizer;

		public IndexModel(IStringLocalizer<LocalizationResources> stringLocalizer)
		{
			this.stringLocalizer = stringLocalizer;
		}

        public string Text { get; set; }
        public string Culture { get; set; }
        public string UiCulture { get; set; }

        public IActionResult OnGet()
		{
			Culture = Thread.CurrentThread.CurrentCulture.Name;
			UiCulture = Thread.CurrentThread.CurrentUICulture.Name;
			Text = stringLocalizer["Passwords_Not_Match"];

			return Page();
		}
	}
}
