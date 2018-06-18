using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace TwoCS.TimeTracker.Domain.Models
{
    public class ModelBase : IModel<string>
    {
        public ModelBase()
        {
            
        }
        public virtual string Id { get; set; }
        public virtual IAudit<string> Audit { get; set; }

        public void SetAudit(HttpContext appContext)
        {
            Audit = new AuditBase(appContext?.User.Identity.Name);
        }
    }

    
}
