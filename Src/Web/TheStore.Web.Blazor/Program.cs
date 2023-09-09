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

	config.AuthenticationPaths.RemoteRegisterPath = authority + "/account/register";
	config.AuthenticationPaths.RemoteProfilePath = authority + "/account/profile";
	config.AuthenticationPaths.LogOutSucceededPath = "/";
	config.ProviderOptions.PostLogoutRedirectUri = config.AuthenticationPaths.LogOutCallbackPath;

	config.ProviderOptions.Authority = authority;
	config.ProviderOptions.ResponseType = "code";
	config.ProviderOptions.ClientId = "TheStore.Web.Blazor";
	config.ProviderOptions.DefaultScopes.Clear();
	config.ProviderOptions.DefaultScopes.Add("openid");
	config.ProviderOptions.DefaultScopes.Add("profile");
});

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();