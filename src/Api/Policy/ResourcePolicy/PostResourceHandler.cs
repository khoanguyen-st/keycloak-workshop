using Api.Context;
using Api.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;

namespace Api.Policy.ResourcePolicy
{
	public class PostResourceHandler : AuthorizationHandler<SameAuthorRequirement, string>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameAuthorRequirement requirement, string authorId)
		{
			if (context.User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value == authorId)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}

	public class SameAuthorRequirement : IAuthorizationRequirement { }
}
