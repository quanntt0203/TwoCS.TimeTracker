namespace TwoCS.TimeTracker.WebApi.Authorizations
{
    using Microsoft.Extensions.DependencyInjection;
    using TwoCS.TimeTracker.Core.Settings;

    public static class AuthorizationExtension
    {
        public static IServiceCollection AddDomainPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperAdmin",
                    policy => policy.Requirements.Add(new RolePermissionRequirment(new string[] { RoleSetting.ROLE_ADMIN })));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ProjectAdmin",
                    policy => policy.Requirements.Add(new RolePermissionRequirment(new string[] { RoleSetting.ROLE_ADMIN })));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserAdmin",
                    policy => policy.Requirements.Add(new RolePermissionRequirment(new string[] { RoleSetting.ROLE_ADMIN, RoleSetting.ROLE_MANAGER })));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("TimeRecordAdmin",
                    policy => policy.Requirements.Add(new RolePermissionRequirment(new string[] { RoleSetting.ROLE_USER, RoleSetting.ROLE_MANAGER })));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ReportAdmin",
                    policy => policy.Requirements.Add(new RolePermissionRequirment(new string[] { RoleSetting.ROLE_ADMIN, RoleSetting.ROLE_USER, RoleSetting.ROLE_MANAGER })));
            });

            return services;
        }
    }
}
