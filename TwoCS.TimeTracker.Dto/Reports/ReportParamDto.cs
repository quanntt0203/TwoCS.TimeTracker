
namespace TwoCS.TimeTracker.Dto.Reports
{
    using System;
    using FluentValidation.Attributes;
    using TwoCS.TimeTracker.Dto.Validators;

    [Validator(typeof(ReportDtoValidator<ReportParamDto>))]
    public class ReportParamDto : DtoBase, IDto
    {
        public ReportTypeEnum ReportType { get; set; }

        public string Project { get; set; }

        public string User { get; set; }

        public ReportGroupTypeEnum GroupBy { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public enum ReportGroupTypeEnum
    {
        Project = 10,
        User = 20
    }

    public class LogTimeTransformDataDto
    {
        public string Project { get; set; }

        public string User { get; set; }

        public string WeekName { get; set; }

        public string MonthName { get; set; }

        public DateTime LogDate { get; set; }

        public int Duration { get; set; }
    }

    public static class ReportLimitionHours
    {
        public const int DayHours = 8;
        public const int WeekHours = 40;
        public const int MonthHours = 160;
    }
}
