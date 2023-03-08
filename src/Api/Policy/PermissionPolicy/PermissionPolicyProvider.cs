using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Api.Policy.PermissionPolicy
{
	public class PermissionPolicyProvider : IAuthorizationPolicyProvider
	{
		const string POLICY_PREFIX = "PermissionAuthorize";

		public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

		public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
		{
			FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
		}

		public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => Task.FromResult(
				new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
					.RequireAuthenticatedUser().Build());

		public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy?>(null);

		public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
		{
			if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
			{
				var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
				policy.AddRequirements(new PermissionAuthorizeRequirement(policyName.Substring(POLICY_PREFIX.Length)));
				return Task.FromResult(policy.Build());
			}
			return Task.FromResult<AuthorizationPolicy?>(null);
		}
	}
}
