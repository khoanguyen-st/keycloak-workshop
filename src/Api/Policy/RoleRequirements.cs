using Microsoft.AspNetCore.Authorization;

namespace Api.Policy
{
	public class RoleRequirements : IAuthorizationRequirement
	{
		public string Role { get; set; }
		public RoleRequirements(string role)
		{
			Role = role;
		}
	}
}
