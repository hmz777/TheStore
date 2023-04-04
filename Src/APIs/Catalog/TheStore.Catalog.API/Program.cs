using System.Reflection;
using TheStore.ApiCommon.Extensions.Migrations;
using TheStore.Catalog.API.Data;
using TheStore.Catalog.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args)
				.RegisterServices<CatalogDbContext>(Assembly.GetExecutingAssembly());

// API-specific services can be registered here

// Pipeline

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	// Apply pending migrations.
	// In production, we use a different strategy.
	app.Migrate<CatalogDbContext>();

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
	var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
	await new DataSeeder().SeedDataAsync(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();