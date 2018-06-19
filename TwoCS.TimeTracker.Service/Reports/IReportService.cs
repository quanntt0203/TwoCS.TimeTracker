namespace TwoCS.TimeTracker.Services
{
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Dto.Reports;

    public interface IReportService
    {
        Task<IReportDto> DailyReportAsync(string userName, ReportParamDto dto);

        Task<IReportDto> WeeklyReportAsync(string userName, ReportParamDto dto);

        Task<IReportDto> MonthlyReportAsync(string userName, ReportParamDto dto);
    }
}
