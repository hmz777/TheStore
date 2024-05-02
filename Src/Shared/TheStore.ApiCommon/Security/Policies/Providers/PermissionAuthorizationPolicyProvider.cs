using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using TheStore.ApiCommon.Security.Policies.Requirements;

namespace TheStore.ApiCommon.Security.Policies.Providers
{
	public class PermissionAuthorizationPolicyProvider : IAuthorizationPolicyProvider
	{
		private readonly DefaultAuthorizationPolicyProvider defaultAuthorizationPolicyProvider;

		public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> authOptions)
		{
			defaultAuthorizationPolicyProvider = new DefaultAuthorizationPolicyProvider(authOptions);
		}

		public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
			 defaultAuthorizationPolicyProvider.GetDefaultPolicyAsync();

		public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() =>
			defaultAuthorizationPolicyProvider.GetFallbackPolicyAsync()!;

		public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
		{
			if (policyName.StartsWith(Permissions.Prefix, StringComparison.InvariantCultureIgnoreCase))
			{
				var policy = new AuthorizationPolicyBuilder();
				policy.AddRequirements(new PermissionRequirement(policyName));

				return Task.FromResult(policy.Build())!;
			}

			return defaultAuthorizationPolicyProvider.GetDefaultPolicyAsync()!;
		}
	}
}