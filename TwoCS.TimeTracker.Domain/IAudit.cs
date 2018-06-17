namespace TwoCS.TimeTracker.Domain
{
    using System;

    public interface IAudit<T> where T : IEquatable<T>
    {
        T CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        T ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
