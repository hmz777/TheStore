using Microsoft.AspNetCore.Components.Authorization;
using Serilog;
using TheStore.Web.BlazorApp.Client.Auth;
using TheStore.Web.BlazorApp.Client.Configuration;

namespace TheStore.Web.BlazorApp.Client.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureClientAuthenticationAndAuthorization(this IServiceCollection services)
        {
            Log.Information("Configure authorization");

            services.AddAuthorizationCore();
            services.AddCascadingAuthenticationState();
            services.AddScoped<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

            return services;
        }

        public static IServiceCollection ConfigureClientHttpClients(this IServiceCollection services, string baseAddress)
        {
            Log.Information("Configure http client");

            services.AddTransient<AntiforgeryHandler>();
            services.AddHttpClient(Constants.HttpClientConstants.ClientBackendHttpClient,
                client => client.BaseAddress = new Uri(baseAddress))
                    .AddHttpMessageHandler<AntiforgeryHandler>();

            services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(Constants.HttpClientConstants.ClientBackendHttpClient));

            return services;
        }
    }
}