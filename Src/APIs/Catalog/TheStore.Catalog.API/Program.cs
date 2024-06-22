using System.Reflection;
using TheStore.ApiCommon.Extensions.Middleware;
using TheStore.ApiCommon.Extensions.Migrations;
using TheStore.Catalog.API.Helpers;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Services;
using static TheStore.ApiCommon.Constants.ConfigurationKeys;

var builder = WebApplication.CreateBuilder(args)
				.RegisterServices<CatalogDbContext>(Assembly.GetExecutingAssembly());

// Pipeline
var app = builder.Build();

app.UseCors("Cors");

if (Environment.GetEnvironmentVariable(Testing.ApplyMigrationsAtRuntime) == "True")
{
	// Apply pending migrations.
	// In production, we use a different strategy.
	app.Migrate<CatalogDbContext>();
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

if (args.Contains("/Seed"))
{
	await app.SeedData(new DataSeeder());
}

if (app.Environment.IsProduction())
{
	app.UseHttpsRedirection();
}

//app.UseAuthorization();

app.MapGrpcServices();

app.MapControllers();

app.Run();

public partial class Program { }