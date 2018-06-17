namespace TwoCS.TimeTracker.Domain.Models
{
    using System;

    public class LogTimeRecord : ModelBase
    {
        public string TimeRecordId { get; set; }

        public string UserId { get; set; }

        public string Remark { get; set; }

        public int Duration { get; set; }

        public DateTime LogTime { get; set; } = DateTime.UtcNow;

        public DateTime LogDate
        {
            get
            {
                return LogTime.Date;
            }
        }

        public virtual TimeRecord TimeRecord { get; set; }

        public virtual User User { get; set; }
    }
}
