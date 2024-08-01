using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Serilog;
using TheStore.Web.BlazorApp.Auth;
using TheStore.Web.BlazorApp.Client.Extensions;
using TheStore.Web.BlazorApp.Configuration;

namespace TheStore.Web.BlazorApp.Extensions
{
    public static class Services
    {
        public static IServiceCollection ConfigureOidc(this IServiceCollection services)
        {
            Log.Information("Configure Oidc");

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookie";
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie("cookie", options =>
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
                options.CheckConsentNeeded = context => true; // TODO: Configure consent
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            return services;
        }

        public static IServiceCollection AddServerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Information("Add server configuration");

            services.AddOptions();
            services.Configure<ServerAppConfig>(configuration.GetRequiredSection(ServerAppConfig.Key));

            services.AddClientConfiguration(configuration);

            return services;
        }

        public static IServiceCollection ConfigureServerHttpClient(this IServiceCollection services)
        {
            Log.Information("Configure http client");

            services.AddHttpClient();

            return services;
        }

        public static IServiceCollection ConfigureBlazorAuthenticationAndAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddCascadingAuthenticationState();
            services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

            return services;
        }
    }
}