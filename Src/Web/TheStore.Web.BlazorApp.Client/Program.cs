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

    builder.Services.ConfigureClientAuthenticationAndAuthorization();
    builder.Services.ConfigureClientHttpClients(builder.HostEnvironment.BaseAddress);
    builder.Services.ConfigureExternalApis();
    builder.Services.ConfigureHelperServices(Assembly.GetExecutingAssembly());
    builder.Services.ConfigureJsonOptions();

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