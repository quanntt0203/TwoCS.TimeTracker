namespace TwoCS.TimeTracker.Dto.Validators
{
    using FluentValidation;
    using TwoCS.TimeTracker.Core.Settings;
    using TwoCS.TimeTracker.Dto;

    public class AddTimeRecordDtoValidator<T> : DtoBaseValidator<T> 
        where T : AddTimeRecordDto
    {
        public AddTimeRecordDtoValidator()
        {
      
            RuleFor(x => x.Name)
                 .NotEmpty()
                 .MaximumLength(ModelSetting.TimeRecord.NameMaxLength);

            RuleFor(x => x.Description)
                .MaximumLength(ModelSetting.TimeRecord.DescriptionMaxLength);

            RuleFor(x => x.ProjectId)
                .NotEmpty();

            RuleFor(x => x.StartTime)
                .NotEmpty();

            RuleFor(x => x.EndTime)
               .NotEmpty();
        }
    }

    public class LogTimeRecordDtoValidator<T> : DtoBaseValidator<T>
        where T : AddLogTimeRecordDto
    {
        public LogTimeRecordDtoValidator()
        {
            RuleFor(x => x.TimeRecordId)
                .NotEmpty();

            RuleFor(x => x.Remark)
                 .NotEmpty()
                 .MaximumLength(ModelSetting.TimeRecord.NameMaxLength);

            RuleFor(x => x.Duration)
                .GreaterThan(0);
        }
    }
}
