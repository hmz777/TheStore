using Microsoft.AspNetCore.Authentication;
using TheStore.Web.BlazorApp.Services;

namespace TheStore.Web.BlazorApp.Middlewares
{
    public class AccessTokenPopulator(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext, AccessTokenDelegator accessTokenDelegator)
        {
            accessTokenDelegator.AccessToken = await httpContext.GetTokenAsync("access_token");

            await next(httpContext);
        }
    }
}