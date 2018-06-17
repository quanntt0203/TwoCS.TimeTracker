namespace TwoCS.TimeTracker.WebApi.Authorizations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class ProjectAdminPermissionRequirment :
        AuthorizationHandler<ProjectAdminPermissionRequirment>, IAuthorizationRequirement
    {
        private readonly string[] _roles;

        public ProjectAdminPermissionRequirment(string[] roles)
        {
            _roles = roles;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectAdminPermissionRequirment requirement)
        {
            await Task.Run(() =>
            {
                var role = context.User.Claims.Where(s => s.Type == "role").FirstOrDefault()?.Value;
                var hasClaim = _roles.Contains(role);
                if (!hasClaim)
                {
                    context.Fail();
                    return;
                }
                else
                {
                    context.Succeed(requirement);
                }
            });
        }
    }
}
