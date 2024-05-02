namespace TheStore.Web.BlazorApp.Components.Identity
{
	public static class IdentityComponentsEndpointRouteBuilderExtensions
	{
		public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
		{
			var identityRouteGroup = endpoints.MapGroup("/Identity");

			return identityRouteGroup;
		}
	}
}