using System.Reflection;
using TheStore.ApiCommon.Extensions.Migrations;
using TheStore.Cart.Infrastructure.Data;
using TheStore.Cart.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args)
	.RegisterServices<CartDbContext>(Assembly.GetExecutingAssembly());

// Pipeline

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	// Apply pending migrations.
	// In production, we use a different strategy.
	app.Migrate<CartDbContext>();

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

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }