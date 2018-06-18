namespace TwoCS.TimeTracker.WebApi.Authorizations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Core.Factories;
    using TwoCS.TimeTracker.Services;

    public class RolePermissionRequirment :
        AuthorizationHandler<RolePermissionRequirment>, IAuthorizationRequirement
    {
        private readonly string[] _roles;

        public RolePermissionRequirment(string[] roles)
        {
            _roles = roles;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RolePermissionRequirment requirement)
        {
            var userService = ResolverFactory.GetService<IUserService>();
            var roles = await userService.GetRolesAsync(context.User.Identity.Name);

            await Task.Run(() =>
            {
                var hasClaim = _roles.Any(s => roles.Contains(s));
                if (!hasClaim)
                {
                    context.Fail();
                    throw new BadRequestException("You are not authorized on this function.");
                }
                else
                {
                    context.Succeed(requirement);
                }
            });
        }
    }
}
