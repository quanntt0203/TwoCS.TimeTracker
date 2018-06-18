namespace TwoCS.TimeTracker.Domain.Models
{
    using Microsoft.AspNetCore.Http;

    public class ModelBase : IModel<string>
    {
        public ModelBase() { }
        public virtual string Id { get; set; }
        public virtual AuditBase Audit { get; set; }

        public void SetAudit(HttpContext appContext)
        {
            Audit = new AuditBase(appContext?.User.Identity.Name);
        }
    }

    
}
