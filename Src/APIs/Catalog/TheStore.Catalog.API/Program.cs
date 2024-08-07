using Serilog;
using Serilog.Templates;
using System.Reflection;
using TheStore.ApiCommon.Extensions.Middleware;
using TheStore.ApiCommon.Extensions.Migrations;
using TheStore.Catalog.API.Helpers;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Extensions;
using static TheStore.ApiCommon.Constants.AppConfiguration;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new ExpressionTemplate(Logging.LoggingTemplate))
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args)
                .RegisterServices<CatalogDbContext>(Assembly.GetExecutingAssembly());

    // Pipeline
    var app = builder.Build();

    if (Environment.GetEnvironmentVariable(Testing.ApplyMigrationsAtRuntimeEnvVarName) == "True")
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

    if (args.Contains("/seed"))
    {
        await app.SeedData(new DataSeeder());
    }

    app.UseCors(Services.CorsPolicyName);

    if (app.Environment.IsProduction())
    {
        app.UseHttpsRedirection();
    }

    app.MapGrpcServices();
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