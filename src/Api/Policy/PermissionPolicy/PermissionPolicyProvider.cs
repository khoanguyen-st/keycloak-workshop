using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Api.Policy.PermissionPolicy
{
	public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
	{
		const string POLICY_PREFIX = "PermissionAuthorize";

		public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

		public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
		{
		}

		public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

		public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

		public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
		{
			var policy = await base.GetPolicyAsync(policyName);

			if (policy != null) return policy;

			if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
			{
				var permission = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);

				permission.AddRequirements(new PermissionAuthorizeRequirement(policyName.Substring(POLICY_PREFIX.Length)));

				return permission.Build();
			}
			return null;
		}
	}
}
