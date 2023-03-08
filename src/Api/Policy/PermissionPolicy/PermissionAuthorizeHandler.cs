using Microsoft.AspNetCore.Authorization;

namespace Api.Policy.PermissionPolicy
{
	public class PermissionAuthorizeHandler : AuthorizationHandler<PermissionAuthorizeRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizeRequirement requirement)
		{
			var permissions = context.User.Claims.Where(c => c.Type == "permission").Select(c => c.Value).ToList();

			if (permissions.Contains(requirement.Permission))
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
