using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TheStore.Web.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(config =>
{
	var authority = "https://localhost:5001";

	config.AuthenticationPaths.LogInPath = "auth/login";
	config.AuthenticationPaths.LogInCallbackPath = "auth/login-callback";
	config.AuthenticationPaths.LogInFailedPath = "auth/login-failed";
	config.AuthenticationPaths.LogOutPath = "auth/logout";
	config.AuthenticationPaths.LogOutCallbackPath = "auth/logout-callback";
	config.AuthenticationPaths.LogOutFailedPath = "auth/logout-failed";
	config.AuthenticationPaths.LogOutSucceededPath = "auth/logged-out";
	config.AuthenticationPaths.ProfilePath = "auth/profile";
	config.AuthenticationPaths.RegisterPath = "auth/register";

	config.AuthenticationPaths.RemoteRegisterPath = authority + "/account/register"; 
	config.AuthenticationPaths.RemoteProfilePath = authority + "/account/profile"; 

	config.ProviderOptions.ResponseType = "code";
	config.ProviderOptions.Authority = authority;
	config.ProviderOptions.ClientId = "TheStore.Web.Blazor";
});

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
