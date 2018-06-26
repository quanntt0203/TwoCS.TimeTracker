namespace TwoCS.TimeTracker.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using TwoCS.TimeTracker.Core.Factories;
    using TwoCS.TimeTracker.Core.Repositories;
    using TwoCS.TimeTracker.Core.UoW;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UnitOfWork(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IUserRepository UserRepository { get => ResolverFactory.GetService<IUserRepository>(); }

        public IProjectRepository ProjectRepository { get => ResolverFactory.GetService<IProjectRepository>(); }

        public ITimeRecordRepository TimeRecordRepository { get => ResolverFactory.GetService<ITimeRecordRepository>(); }
    }
}
