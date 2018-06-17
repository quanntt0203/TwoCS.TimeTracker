namespace TwoCS.TimeTracker.Domain
{
    using System;

    public interface IModel<T> where T : IEquatable<T>
    {
        T Id { get; set; }
        IAudit<T> Audit { get; set; }
    }
}
