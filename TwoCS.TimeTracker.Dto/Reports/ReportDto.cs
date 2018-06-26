namespace TwoCS.TimeTracker.Dto.Reports
{
    using System.Collections.Generic;

    public interface IReportData
    {
        int Duration { get; set; }

        bool IsMarked { get; set; }
    }

    public class ReportDataDto : IReportData
    {
        public bool IsMarked { get; set; }
        public int Duration { get; set; }
    }

    public interface IReportDto
    {
        ReportTypeEnum ReportType { get; set; }
        ReportParamDto Param { get; set; }
        IEnumerable<IReportData> Records { get; set; }
    }

    public class ReportDto : IReportDto
    {
        public ReportTypeEnum ReportType { get; set; }
        public ReportParamDto Param { get; set; }
        public IEnumerable<IReportData> Records { get; set; }
    }
}
