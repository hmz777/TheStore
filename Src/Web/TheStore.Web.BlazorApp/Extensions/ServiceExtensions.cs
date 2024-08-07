using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Serilog;
using TheStore.Web.BlazorApp.Auth;
using TheStore.Web.BlazorApp.Client.Auth;
using TheStore.Web.BlazorApp.Client.Configuration;

namespace TheStore.Web.BlazorApp.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureOidc(this IServiceCollection services)
        {
            Log.Information("Configure Oidc");

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookie";
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie("Cookie", options =>
            {
                options.Cookie.Name = "TSCookie";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.HttpOnly = true;
                options.DataProtectionProvider = DataProtectionProvider.Create("The Store");
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://localhost:7575";

                options.ClientId = "TheStore.Web.Blazor";
                options.ClientSecret = "secret";
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.ResponseMode = OpenIdConnectResponseMode.Query;

                options.Scope.Clear();
                options.Scope.Add(OpenIdConnectScope.OpenIdProfile);

                options.MapInboundClaims = false;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;
                options.CheckConsentNeeded = context => false; // TODO: Configure consent
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            return services;
        }

        public static IServiceCollection ConfigureExternalApisEndpoints(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Information("Add server configuration");

            services.AddOptions();
            services.Configure<EndpointsConfig>(configuration.GetRequiredSection(EndpointsConfig.Key));

            return services;
        }

        public static WebApplicationBuilder ConfigureServerHttpClients(this WebApplicationBuilder builder)
        {
            Log.Information("Configure http client");

            builder.Services.AddTransient<AuthDelegatingHandler>();
            builder.Services.AddTransient<AntiforgeryHandler>();

            var endpointsConfig = builder.Configuration.GetSection(EndpointsConfig.Key).Get<EndpointsConfig>();

            if (endpointsConfig == null)
            {
                // TODO: Report error
                throw new Exception("Endpoint configuration couldn't be parsed");
            }

            builder.Services.AddHttpClient(Constants.HttpClientConstants.ServerCatalogHttpClient, c =>
                c.BaseAddress = new Uri(endpointsConfig.GetCatalogEndpoint().Url))
                .AddHttpMessageHandler<AntiforgeryHandler>();

            builder.Services.AddHttpClient(Constants.HttpClientConstants.ServerAuthenticatedCartHttpClient,
                c => c.BaseAddress = new Uri(endpointsConfig.GetCartEndpoint().Url))
                .AddHttpMessageHandler<AuthDelegatingHandler>()
                .AddHttpMessageHandler<AntiforgeryHandler>();

            return builder;
        }

        public static IServiceCollection ConfigureServerAuthenticationAndAuthorization(this IServiceCollection services)
        {
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddCascadingAuthenticationState();
            services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();
            services.ConfigureOidc();

            return services;
        }
    }
}