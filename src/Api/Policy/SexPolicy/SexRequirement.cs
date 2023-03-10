using Microsoft.AspNetCore.Authorization;

namespace Api.Policy.SexPolicy
{
	public class SexRequirement : IAuthorizationRequirement
	{
		public SexRequirement(string sex) =>
		Sex = sex;

		public string Sex { get; }
	}
}
