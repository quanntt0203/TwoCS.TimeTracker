namespace TwoCS.TimeTracker.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Core;
    using TwoCS.TimeTracker.Domain.Models;
    using TwoCS.TimeTracker.Dto;

    public interface ITimeRecordService : IService<TimeRecord>
    {
        Task<TimeRecordDto> CreateAsync(string userName, AddTimeRecordDto dto);

        Task<LogTimeRecordDto> LogTimeAsync(string userName, AddLogTimeRecordDto dto);

        Task<IEnumerable<TimeRecordDto>> SearchAsync(string userName);

    }
}
