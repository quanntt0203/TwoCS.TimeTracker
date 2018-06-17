namespace TwoCS.TimeTracker.Dto
{
    using System;

    public class TimeRecordDto : DtoBase
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ProjectId { get; set; }

        public string UserId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int Duration { get; set; }

        public string CapturedInfo { get; set; }

        public virtual ProjectDto Project { get; set; }

        public virtual UserDto User { get; set; }
    }
}
