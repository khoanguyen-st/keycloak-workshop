using Microsoft.AspNetCore.Authorization;

namespace Api.Policy.PermissionPolicy
{
	public class PermissionAuthorizeRequirement : IAuthorizationRequirement
	{
		public string Permission { get; set; }
		public PermissionAuthorizeRequirement(string permission)
		{
			Permission = permission;
		}
	}
}
