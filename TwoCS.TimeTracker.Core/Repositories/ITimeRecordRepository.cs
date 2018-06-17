﻿namespace TwoCS.TimeTracker.Core.Repositories
{
    using TwoCS.TimeTracker.Domain.Models;

    public interface ITimeRecordRepository : IRepository<TimeRecord>
    {
    }

    public interface ILogTimeRecordRepository : IRepository<LogTimeRecord>
    {
    }
}
