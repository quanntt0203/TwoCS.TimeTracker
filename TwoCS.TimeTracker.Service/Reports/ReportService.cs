namespace TwoCS.TimeTracker.Services
{
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Dto.Reports;

    public class ReportService : IReportService
    {
        public async Task<IReportDto> DailyReportAsync(string userName, ReportParamDto dto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReportDto> MonthlyReportAsync(string userName, ReportParamDto dto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReportDto> WeeklyReportAsync(string userName, ReportParamDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
