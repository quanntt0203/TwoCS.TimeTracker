namespace TwoCS.TimeTracker.Services
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using TwoCS.TimeTracker.Core.UoW;

    public static class ServiceExtension
    {
        public static IServiceCollection AddDomainServies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IProxyService, ProxyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITimeRecordService, TimeRecordService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;

        }
    }
}
