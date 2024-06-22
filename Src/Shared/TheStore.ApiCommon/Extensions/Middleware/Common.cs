using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TheStore.ApiCommon.Interfaces;

namespace TheStore.ApiCommon.Extensions.Middleware
{
	public static class Common
	{
		public async static Task<WebApplication> SeedData<TContext>(this WebApplication webApplication, IDataSeeder<TContext> dataSeeder)
			where TContext : DbContext
		{
			Log.Information($"Seeding data using context {typeof(TContext)}...");

			using (var scope = webApplication.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<TContext>();
				await dataSeeder.SeedDataAsync(context);
			}

			Log.Information($"Data successfully seeded using context {typeof(TContext)}");

			return webApplication;
		}
	}
}