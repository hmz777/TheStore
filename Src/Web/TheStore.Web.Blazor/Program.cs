using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TheStore.Web.Blazor;
using TheStore.Web.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(config =>
{
	// TODO: W e get the auth server url from a configuration endpoint
	var authority = "https://localhost:5001";
	config.ProviderOptions.Authority = authority;

	config.AuthenticationPaths.RemoteRegisterPath = config.ProviderOptions.Authority + "/account/register";
	config.AuthenticationPaths.RemoteProfilePath = config.ProviderOptions.Authority + "/account/profile";
	config.AuthenticationPaths.LogOutSucceededPath = "/";
	config.ProviderOptions.PostLogoutRedirectUri = config.AuthenticationPaths.LogOutCallbackPath;

	config.ProviderOptions.ResponseType = "code";
	config.ProviderOptions.ClientId = "TheStore.Web.Blazor";
	config.ProviderOptions.DefaultScopes.Clear();
	config.ProviderOptions.DefaultScopes.Add("openid");
	config.ProviderOptions.DefaultScopes.Add("profile");
});

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<CartService>();

await builder.Build().RunAsync();