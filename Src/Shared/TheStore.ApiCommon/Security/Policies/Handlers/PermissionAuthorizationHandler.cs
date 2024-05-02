using Microsoft.AspNetCore.Authorization;
using TheStore.ApiCommon.Security.Policies.Requirements;

namespace TheStore.ApiCommon.Security.Policies.Handlers
{
	public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
		{
			var readClaim = context.User.FindFirst(c =>
									c.Type == Permissions.Type &&
									c.Value == requirement.Permission);

			if (readClaim != null)
			{
				context.Succeed(requirement);
			}
			else
			{
				context.Fail();
			}

			return Task.CompletedTask;
		}
	}
}