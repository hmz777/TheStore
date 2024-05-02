using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace TheStore.Web.BlazorApp.Extensions
{
    public static class Auth
    {
        public static WebApplicationBuilder ConfigureOidc(this WebApplicationBuilder builder)
        {
            builder.Services.AddBff();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookie";
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie("cookie", options =>
            {
                options.Cookie.Name = "TSCookie";
                options.Cookie.SameSite = SameSiteMode.Lax;
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

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            return builder;
        }
    }
}