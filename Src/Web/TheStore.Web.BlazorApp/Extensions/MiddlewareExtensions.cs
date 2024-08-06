using TheStore.Web.BlazorApp.Middlewares;

namespace TheStore.Web.BlazorApp.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseAccessTokenPopulator(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<AccessTokenPopulator>();

            return applicationBuilder;
        }
    }
}