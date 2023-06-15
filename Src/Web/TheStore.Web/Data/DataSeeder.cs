using Microsoft.AspNetCore.Identity;
using TheStore.Web.Domain.Users;

namespace TheStore.Web.Data
{
	public class DataSeeder
	{
		public const string MasterAdminUsername = "masteradmin";
		public const string MasterAdminEmail = "masteradmin@email.com";
		public const string MasterAdminPassword = "master@pass@admin@word@123";

		public async static Task SeedAsync(ApplicationDbContext applicationDbContext, UserManager<AppUser> userManager)
		{
			var masterAdmin = await userManager.FindByNameAsync(MasterAdminUsername);

			if (masterAdmin == null)
			{
				var newMasterAdmin = new AppUser
				{
					FullName = "Master Admin",
					UserName = MasterAdminUsername,
					Email = MasterAdminEmail
				};

				var result = await userManager.CreateAsync(newMasterAdmin, MasterAdminPassword);

				if (result.Errors.Any())
				{
					throw new Exception($"Failed to create master admin! Errors:{string.Join(',', result.Errors.Select(ie => ie.Description))}");
				}
			}
		}
	}
}
