using Microsoft.AspNetCore.Authorization;

namespace TheStore.ApiCommon.Security.Policies.Requirements
{
	public class PermissionRequirement : IAuthorizationRequirement
	{
		public string Permission { get; }

		public PermissionRequirement(string permission)
		{
			Permission = permission;
		}
	}
}