using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using System.Reflection;
using TheStore.Web.BlazorApp.Client.Extensions;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateLogger();

try
{
	var builder = WebAssemblyHostBuilder.CreateDefault(args);

	builder.Services.AddClientConfiguration(builder.Configuration);

	builder.Services.ConfigureAuthorization();

	builder.Services.ConfigureHttpClient(builder.HostEnvironment.BaseAddress);

	builder.Services.ConfigureApis();

	builder.Services.ConfigureHelperServices(Assembly.GetExecutingAssembly());

	await builder.Build().RunAsync();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush();
}