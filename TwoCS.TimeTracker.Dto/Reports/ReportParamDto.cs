
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

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
