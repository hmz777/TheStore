using Microsoft.AspNetCore.Identity;

namespace TheStore.Web.Domain.Users
{
	public class AppUser : IdentityUser
	{
		public string FullName { get; set; }
	}
}
