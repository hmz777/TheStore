using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.ApiCommon.Extensions.Migrations;

namespace TheStore.ApiCommon.Extensions.Pipeline
{
	public static class Common
	{
		public static WebApplication BuildAndRunPipeline<TContext>(this WebApplicationBuilder builder) where TContext : DbContext
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

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();

			return app;
		}
	}
}
