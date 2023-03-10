using Microsoft.AspNetCore.Authorization;

namespace Api.Policy.AgePolicy
{
	public class AgeHandler : AuthorizationHandler<AgeRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
		{
			var dateOfBirthClaim = context.User.FindFirst(c => c.Type == "DOB")?.Value;

			if(dateOfBirthClaim == null)
			{
				return Task.CompletedTask;
			}

			var dateOfBirth = DateTime.Parse(dateOfBirthClaim);

			int calculatedAge = DateTime.Now.Year - dateOfBirth.Year;

			if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
			{
				calculatedAge--;
			}

			if (calculatedAge >= requirement.MinimumAge)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
