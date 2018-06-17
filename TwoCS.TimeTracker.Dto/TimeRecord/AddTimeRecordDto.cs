namespace TwoCS.TimeTracker.Dto
{
    using System;
    using FluentValidation.Attributes;
    using TwoCS.TimeTracker.Dto.Validators;

    [Validator(typeof(AddTimeRecordDtoValidator<AddTimeRecordDto>))]
    public class AddTimeRecordDto : DtoBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ProjectId { get; set; }

        public string UserId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }

    [Validator(typeof(LogTimeRecordDtoValidator<AddLogTimeRecordDto>))]
    public class AddLogTimeRecordDto : DtoBase
    {
        public string TimeRecordId { get; set; }

        public string Remark { get; set; }

        public int Duration { get; set; }
    }

    public class LogTimeRecordDto
    {
        public string TimeRecordId { get; set; }

        public string UserId { get; set; }

        public string Remark { get; set; }

        public int Duration { get; set; }

        public DateTime LogTime { get; set; }

        public DateTime LogDate
        {
            get
            {
                return LogTime.Date;
            }
        }

        public virtual TimeRecordDto TimeRecord { get; set; }

        public virtual UserDto User { get; set; }
    }
}
