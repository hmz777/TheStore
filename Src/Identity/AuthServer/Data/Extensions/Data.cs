using AuthServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Data.Extensions
{
	public static class Data
	{
		public static async Task MigrateAndSeed(this WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

				dbContext.Database.EnsureDeleted();
				dbContext.Database.Migrate();

				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
				var hasUsers = await userManager.Users.AnyAsync();

				if (hasUsers == false)
				{
					await userManager.CreateAsync(new ApplicationUser
					{
						FirstName = "Test 1",
						LastName = "Test 2",
						Email = "test@test.com",
						UserName = "testUser",
						PhoneNumber = "+4911111111111",
						PhoneNumberConfirmed = false,
						EmailConfirmed = true,
						Active = true,
					}, "Password@123");
				}
			}
		}
	}
}