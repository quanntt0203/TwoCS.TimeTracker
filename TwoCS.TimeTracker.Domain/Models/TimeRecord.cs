namespace TwoCS.TimeTracker.Domain.Models
{
    using System;
    using System.Collections.Generic;

    public class TimeRecord : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string ProjectId { get; set; }

        public string UserId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime StartDate
        {
            get
            {
                return StartTime.Date;
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return EndTime?.Date;
            }
        }

        public string CapturedInfo { get; set; }

        public virtual Project Project { get; set; }

        public virtual User User { get; set; }

        public virtual List<LogTimeRecord> LogTimeRecords { get; set; }
    }
}
