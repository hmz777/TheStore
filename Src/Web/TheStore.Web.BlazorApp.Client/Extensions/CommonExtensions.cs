using Serilog;
using System.Reflection;
using System.Text.Json;
using TheStore.Web.BlazorApp.Client.Services;

namespace TheStore.Web.BlazorApp.Client.Extensions
{
    public static class CommonExtensions
    {
        public static IServiceCollection ConfigureHelperServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            Log.Information("Configure helper services");

            services.AddMediatR(config => config.RegisterServicesFromAssemblies(assemblies));
            services.AddScoped<EventBroker>();

            return services;
        }

        public static IServiceCollection ConfigureJsonOptions(this IServiceCollection services)
        {
            Log.Information("Add client configuration");

            services.AddOptions();
            services.Configure<JsonSerializerOptions>(jsonOptions => jsonOptions.PropertyNameCaseInsensitive = true);

            return services;
        }

        public static IServiceCollection ConfigureExternalApis(this IServiceCollection services)
        {
            Log.Information("Configure APIs");

            services.AddScoped<CatalogService>();
            services.AddScoped<CartService>();

            return services;
        }
    }
}