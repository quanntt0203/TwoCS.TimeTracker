namespace TwoCS.TimeTracker.Dto.Reports
{
    public class WeeklyReportDto : ReportDataDto
    {
        public string Project { get; set; }
        public string User { get; set; }
        public string WeekName { get; set; }
    }

    
}
