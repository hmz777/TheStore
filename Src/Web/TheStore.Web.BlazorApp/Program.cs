using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Reflection;
using TheStore.ApiCommon.Extensions.Services;
using TheStore.Web.BlazorApp.Auth;
using TheStore.Web.BlazorApp.Client.Extensions;
using TheStore.Web.BlazorApp.Components;
using TheStore.Web.BlazorApp.Components.Identity;
using TheStore.Web.BlazorApp.Extensions;

try
{
	var builder = WebApplication.CreateBuilder(args);

	builder.ConfigureLogging();

	builder.Services.AddServerConfiguration(builder.Configuration);

	// Add services to the container.
	builder.Services.AddRazorComponents()
		.AddInteractiveWebAssemblyComponents();

	builder.Services.AddAuthorization();
	builder.Services.AddAuthentication().AddIdentityCookies();
	builder.Services.AddCascadingAuthenticationState();
	builder.Services.AddScoped<AuthenticationStateProvider, NoOpAuthenticationStateProvider>();

	builder.ConfigureOidc();

	builder.Services.ConfigureHelperServices(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(BffAuthenticationStateProvider))!);
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
	app.UseAntiforgery();

	app.UseAuthentication();
	app.UseBff();
	app.UseAuthorization();

	app.MapBffManagementEndpoints();

	app.MapRazorComponents<App>()
		.AddInteractiveWebAssemblyRenderMode()
		.AddAdditionalAssemblies(typeof(TheStore.Web.BlazorApp.Client._Imports).Assembly);

	app.MapAdditionalIdentityEndpoints();

	app.Run();
}
catch (Exception ex)
{
	Log.Error(ex, ex.Message);
}
finally
{
	Log.CloseAndFlush();
}
