namespace TwoCS.TimeTracker.Domain.Models
{
    using System;

    public class AuditBase : IAudit<long>
    {
        public long CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
