namespace TwoCS.TimeTracker.Services
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceExtension
    {
        public static IServiceCollection AddDomainServie(this IServiceCollection services)
        {

            services.AddScoped<IProxyService, ProxyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITimeRecordService, TimeRecordService>();

            return services;

        }
    }
}
