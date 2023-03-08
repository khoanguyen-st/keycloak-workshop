using Microsoft.AspNetCore.Authorization;

namespace Api.Policy.PermissionPolicy
{
	public class PermissionAuthorizeAttribute : AuthorizeAttribute
	{
		const string POLICY_PREFIX = "PermissionAuthorize";

		public PermissionAuthorizeAttribute(string permission) => Permission = permission;

		public string Permission
		{
			get
			{
				return Policy?.Substring(POLICY_PREFIX.Length);
			}
			set
			{
				Policy = $"{POLICY_PREFIX}{value}";
			}
		}
	}
}
