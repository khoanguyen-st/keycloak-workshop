using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace Api.Policy.RolePolicy
{
    public class RoleHandler : AuthorizationHandler<RoleRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirements requirement)
        {
            var resourceAccess = context.User.FindFirst(c => c.Type == "resource_access")?.Value;

            if (resourceAccess == null)
            {
                return Task.CompletedTask;
            }

            var resourceAccessObject = JObject.Parse(resourceAccess);

            var roleList = resourceAccessObject.SelectToken("my-app.roles")?.ToList();

            if (roleList == null)
            {
                return Task.CompletedTask;
            }

            var strList = new List<string>();

            foreach (var item in roleList)
            {
                strList.Add(item.ToString());
            }

            if (strList.Contains(requirement.Role))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
