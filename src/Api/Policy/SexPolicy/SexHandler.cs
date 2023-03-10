using Api.Policy.AgePolicy;
using Microsoft.AspNetCore.Authorization;

namespace Api.Policy.SexPolicy
{
	public class SexHandler : AuthorizationHandler<SexRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SexRequirement requirement)
		{
			var sexClaim = context.User.FindFirst(c => c.Type == "sex")?.Value;

			if(sexClaim == null)
			{
				return Task.CompletedTask;
			}

			if(sexClaim == requirement.Sex)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
