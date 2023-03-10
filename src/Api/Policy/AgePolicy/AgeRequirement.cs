using Microsoft.AspNetCore.Authorization;

namespace Api.Policy.AgePolicy
{
	public class AgeRequirement : IAuthorizationRequirement
	{
		public AgeRequirement(int minimumAge) =>
		MinimumAge = minimumAge;

		public int MinimumAge { get; }
	}
}
