using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;
using TheStore.Web.BlazorApp.Client.Auth;
using TheStore.Web.BlazorApp.Client.Configuration;
using TheStore.Web.BlazorApp.Client.Services;

namespace TheStore.Web.BlazorApp.Client.Extensions
{
	public static class Services
	{
		public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
		{
			services.AddAuthorizationCore();
			services.AddCascadingAuthenticationState();
			services.AddScoped<AuthenticationStateProvider, BffAuthenticationStateProvider>();
			services.AddTransient<AntiforgeryHandler>();

			return services;
		}

		public static IServiceCollection ConfigureHttpClient(this IServiceCollection services, string baseAddress)
		{
			services.AddHttpClient("Backend", client => client.BaseAddress = new Uri(baseAddress))
					.AddHttpMessageHandler<AntiforgeryHandler>();

			services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Backend"));

			return services;
		}

		public static IServiceCollection AddClientConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddOptions();
			services.Configure<ClientAppConfig>(configuration.GetRequiredSection(ClientAppConfig.Key));

			return services;
		}

		public static IServiceCollection ConfigureHelperServices(this IServiceCollection services, params Assembly[] assemblies)
		{
			services.AddMediatR(config => config.RegisterServicesFromAssemblies(assemblies));
			services.AddAutoMapper(assemblies);
			services.AddScoped<EventBroker>();

			return services;
		}

		public static IServiceCollection ConfigureApis(this IServiceCollection services)
		{
			services.AddScoped<CatalogService>();
			services.AddScoped<CartService>();

			return services;
		}
	}
}