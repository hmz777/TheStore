using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TheStore.ApiCommon.Security.Policies.Handlers;
using TheStore.ApiCommon.Security.Policies.Providers;

namespace TheStore.ApiCommon.Extensions.Security
{
	public static class Common
	{
		public static WebApplicationBuilder ConfigureAuthorizationPolicies(this WebApplicationBuilder webApplicationBuilder)
		{
			var services = webApplicationBuilder.Services;

			services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
			services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

			return webApplicationBuilder;
		}
	}
}
