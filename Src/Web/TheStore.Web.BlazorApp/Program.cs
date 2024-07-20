using Serilog;
using System.Reflection;
using TheStore.ApiCommon.Extensions.Services;
using TheStore.Web.BlazorApp.Auth;
using TheStore.Web.BlazorApp.Client.Extensions;
using TheStore.Web.BlazorApp.Components;
using TheStore.Web.BlazorApp.Components.Identity;
using TheStore.Web.BlazorApp.Extensions;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateLogger();

try
{
	var builder = WebApplication.CreateBuilder(args);

	builder.ConfigureLogging();

	builder.Services.AddServerConfiguration(builder.Configuration);

	// Add services to the container.
	builder.Services.AddRazorComponents()
		.AddInteractiveWebAssemblyComponents();

	builder.Services.ConfigureOidc();
	builder.Services.ConfigureBlazorAuthenticationAndAuthorization();

	builder.Services.ConfigureHelperServices(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(BffAuthenticationStateProvider))!);
	builder.Services.ConfigureServerHttpClient();
	builder.Services.ConfigureApis();

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseWebAssemblyDebugging();
	}
	else
	{
		app.UseExceptionHandler("/Error", createScopeForErrors: true);
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}

	app.UseHttpsRedirection();

	app.UseStaticFiles();

	app.UseAuthentication();
	app.UseAuthorization();
	app.UseAntiforgery();

	app.MapRazorComponents<App>()
		.AddInteractiveWebAssemblyRenderMode()
		.AddAdditionalAssemblies(typeof(TheStore.Web.BlazorApp.Client._Imports).Assembly);

	app.MapAdditionalIdentityEndpoints();

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
