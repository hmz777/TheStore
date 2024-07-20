using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace TheStore.ApiCommon.Extensions.Migrations
{
	public static class Common
	{
		public static void Migrate<TContext>(this WebApplication webApplication) where TContext : DbContext
		{
			Log.Warning($"Runtime database migration flag is set, migrating with context {typeof(TContext).Name}...");

			var services = webApplication.Services;

			using (var scope = services.CreateScope())
			{
				var serviceProvider = scope.ServiceProvider;
				var context = serviceProvider.GetRequiredService<TContext>();

				context.Database.EnsureDeleted();
				context.Database.Migrate();
			}

			Log.Information($"Database successfully migrated with context {typeof(TContext).Name}");
		}
	}
}