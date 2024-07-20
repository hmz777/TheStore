using Microsoft.AspNetCore.Components.Authorization;
using Serilog;
using System.Reflection;
using TheStore.Web.BlazorApp.Client.Auth;
using TheStore.Web.BlazorApp.Client.Configuration;
using TheStore.Web.BlazorApp.Client.Services;

namespace TheStore.Web.BlazorApp.Client.Extensions
{
	public static class Services
	{
		public static IServiceCollection ConfigureBlazorClientAuthenticationAndAuthorization(this IServiceCollection services)
		{
			Log.Information("Configure authorization");

			services.AddAuthorizationCore();
			services.AddCascadingAuthenticationState();
			services.AddScoped<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

			return services;
		}

		public static IServiceCollection ConfigureClientHttpClient(this IServiceCollection services, string baseAddress)
		{
			Log.Information("Configure http client");

			services.AddHttpClient("Backend", client => client.BaseAddress = new Uri(baseAddress));
			//.AddHttpMessageHandler<AntiforgeryHandler>();

			services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Backend"));

			return services;
		}

		public static IServiceCollection AddClientConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			Log.Information("Add client configuration");

			services.AddOptions();
			services.Configure<ClientAppConfig>(configuration.GetRequiredSection(ClientAppConfig.Key));

			return services;
		}

		public static IServiceCollection ConfigureHelperServices(this IServiceCollection services, params Assembly[] assemblies)
		{
			Log.Information("Configure helper services");

			services.AddMediatR(config => config.RegisterServicesFromAssemblies(assemblies));
			services.AddAutoMapper(assemblies);
			services.AddScoped<EventBroker>();

			return services;
		}

		public static IServiceCollection ConfigureApis(this IServiceCollection services)
		{
			Log.Information("Configure APIs");

			services.AddScoped<CatalogService>();
			services.AddScoped<CartService>();

			return services;
		}
	}
}