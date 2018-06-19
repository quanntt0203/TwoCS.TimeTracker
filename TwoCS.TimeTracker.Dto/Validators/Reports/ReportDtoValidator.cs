namespace TwoCS.TimeTracker.Dto.Validators
{
    using FluentValidation;
    using TwoCS.TimeTracker.Dto.Reports;

    public class ReportDtoValidator<T> : DtoBaseValidator<T> 
        where T : ReportParamDto
    {
        public ReportDtoValidator()
        {
            RuleFor(x => x.ReportType)
                 .NotEmpty();

            //TODO: start/end dates
            //RuleFor(x => x.EndDate)
            //    .Must(s => !s.HasValue || s.HasValue && s.Value >= this.StartDate);

        }
    }
}
