namespace TwoCS.TimeTracker.Core
{
    using TwoCS.TimeTracker.Domain.Models;

    public interface IStorageCollection<T> : IRepository<T> where T : ModelBase
    {
    }
}
