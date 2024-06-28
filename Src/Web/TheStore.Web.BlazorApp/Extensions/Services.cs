using Serilog;
using TheStore.Web.BlazorApp.Client.Extensions;
using TheStore.Web.BlazorApp.Configuration;

namespace TheStore.Web.BlazorApp.Extensions
{
	public static class Services
	{
		public static IServiceCollection AddServerConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			Log.Information("Add server configuration");

			services.AddOptions();
			services.Configure<ServerAppConfig>(configuration.GetRequiredSection(ServerAppConfig.Key));

			services.AddClientConfiguration(configuration);

			return services;
		}
	}
}
