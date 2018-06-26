namespace TwoCS.TimeTracker.Core.UoW
{
    using TwoCS.TimeTracker.Core.Repositories;

    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IProjectRepository ProjectRepository { get; }

        ITimeRecordRepository TimeRecordRepository { get; }
    }
}
