namespace TwoCS.TimeTracker.Dto.Reports
{
    public class MonthlyReportDto : ReportDataDto
    {
        public string Project { get; set; }
        public string User { get; set; }
        public string MonthName { get; set; }
    }
}
