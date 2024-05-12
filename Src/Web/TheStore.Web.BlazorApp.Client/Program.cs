using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;
using TheStore.Web.BlazorApp.Client.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddClientConfiguration(builder.Configuration);

builder.Services.ConfigureAuthorization();

builder.Services.ConfigureHttpClient(builder.HostEnvironment.BaseAddress);

builder.Services.ConfigureApis();

builder.Services.ConfigureHelperServices(Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();