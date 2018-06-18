namespace TwoCS.TimeTracker.Data
{
    using Core.Repositories;
    using Microsoft.Extensions.DependencyInjection;
    using Mongo;
    using TwoCS.TimeTracker.Core;
    using TwoCS.TimeTracker.Domain.Models;

    public static class DataExtension
    {
        public static IServiceCollection AddDomainRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddScoped<ITimeRecordRepository, TimeRecordRepository>();

            services.AddScoped<ILogTimeRecordRepository, LogTimeRecordRepository>();

            return services;
        }
    }
}
