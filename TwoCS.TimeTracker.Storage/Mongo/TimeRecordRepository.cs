namespace TwoCS.TimeTracker.Data.Mongo
{
    using Core.Repositories;
    using Domain.Models;

    public class TimeRecordRepository : MongoRepository<TimeRecord>, ITimeRecordRepository
    {
        
    }

    public class LogTimeRecordRepository : MongoRepository<LogTimeRecord>, ILogTimeRecordRepository
    {

    }
}
