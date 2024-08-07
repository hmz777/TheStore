using Microsoft.AspNetCore.Authentication;

namespace TheStore.Web.BlazorApp.Extensions
{
    public static class HttpContextExtensions
    {
        public static AuthenticationProperties? GetAuthenticationProperties(this HttpContext httpContext)
        {
            return httpContext.Features.Get<IAuthenticateResultFeature>()?.AuthenticateResult?.Properties;
        }

        public static async Task<string?> GetAccessToken(this HttpContext httpContext)
        {
            return await httpContext.GetTokenAsync("access_token");
        }
    }
}