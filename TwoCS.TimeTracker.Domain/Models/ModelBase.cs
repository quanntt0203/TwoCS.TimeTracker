namespace TwoCS.TimeTracker.Domain.Models
{
    public class ModelBase : IModel<string>
    {
        public virtual string Id { get; set; }
        public virtual IAudit<string> Audit { get; set; }
    }
}
