namespace TwoCS.TimeTracker.Dto.Reports
{
    using System;

    public class DailyReportDto : ReportDataDto
    {
        public string Project { get; set; }
        public string User { get; set; }
        public DateTime LogDate { get; set; }

        public string Date
        {
            get
            {
                return LogDate.ToString("dd/MM/yyyy");
            }
        }
    }
}
