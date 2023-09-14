using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthServer.Pages.Account.Profile.Password
{
	public class IndexModel : PageModel
	{
		public UpdatePasswordViewModel View { get; set; }
		
		public void OnGet()
		{
		}
	}
}
