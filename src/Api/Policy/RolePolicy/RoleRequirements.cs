using Microsoft.AspNetCore.Authorization;

namespace Api.Policy.RolePolicy
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
