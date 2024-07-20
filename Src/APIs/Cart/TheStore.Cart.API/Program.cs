using Serilog;
using Serilog.Templates;
using System.Reflection;
using TheStore.ApiCommon.Extensions.Migrations;
using TheStore.Cart.Infrastructure.Data;
using TheStore.Cart.Infrastructure.Services;
using static TheStore.ApiCommon.Constants.AppConfiguration;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console(new ExpressionTemplate(Logging.LoggingTemplate))
	.CreateLogger();

try
{
	var builder = WebApplication.CreateBuilder(args)
	.RegisterServices<CartDbContext>(Assembly.GetExecutingAssembly());

	// Pipeline

	var app = builder.Build();

	app.UseCors("Cors");

	if (Environment.GetEnvironmentVariable(Testing.ApplyMigrationsAtRuntimeEnvVarName) == "True")
	{
		// Apply pending migrations.
		// In production, we use a different strategy.
		app.Migrate<CartDbContext>();
	}

	if (app.Environment.IsDevelopment())
	{
		// Swagger
		app.UseSwagger();
		app.UseSwaggerUI(options =>
		{
			options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
			options.RoutePrefix = string.Empty;
		});
	}

	using (var scope = app.Services.CreateScope())
	{
		var context = scope.ServiceProvider.GetRequiredService<CartDbContext>();
		await new DataSeeder().SeedDataAsync(context);
	}

	app.MapControllers();

	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush();
}

public partial class Program;
