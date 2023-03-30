using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheStore.ApiCommon.Extensions.Migrations;
using TheStore.ApiCommon.Interfaces;

namespace TheStore.ApiCommon.Extensions.Pipeline
{
	public static class Common
	{
		public static WebApplication BuildAndRunPipeline<TContext>(
			this WebApplicationBuilder builder,
				 IDataSeeder? dataSeeder = null) where TContext : DbContext
		{
			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				// Apply pending migrations.
				// In production, we use a different strategy.
				app.Migrate<TContext>();

				// Swagger
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
					options.RoutePrefix = string.Empty;
				});
			}

			if (dataSeeder != null)
			{
				using (var scope = app.Services.CreateScope())
				{
					var context = scope.ServiceProvider.GetRequiredService<TContext>();
					dataSeeder?.SeedData(context);
				}
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();

			return app;
		}
	}
}
