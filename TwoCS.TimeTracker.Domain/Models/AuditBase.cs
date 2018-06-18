namespace TwoCS.TimeTracker.Domain.Models
{
    using System;

    public class AuditBase : IAudit<string>
    {
        public AuditBase():this(null)
        {

        }

        public AuditBase(string createdBy)
        {
            CreatedBy = createdBy;
        }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; }
    }
}
