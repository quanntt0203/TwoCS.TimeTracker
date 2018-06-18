namespace TwoCS.TimeTracker.Domain
{
    using System;
    using Models;

    public interface IModel<T> where T : IEquatable<T>
    {
        T Id { get; set; }
        AuditBase Audit { get; set; }
    }
}
