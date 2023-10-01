using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TheStore.ApiCommon.Data.Helpers;

namespace TheStore.ApiCommon.Extensions.Migrations
{
	public static class Common
	{
		public static void Migrate<TContext>(this WebApplication webApplication) where TContext : DbContext
		{
			var services = webApplication.Services;

			using (var scope = services.CreateScope())
			{
				var serviceProvider = scope.ServiceProvider;
				var context = serviceProvider.GetRequiredService<TContext>();

				context.Database.Migrate();
			}
		}
	}
}