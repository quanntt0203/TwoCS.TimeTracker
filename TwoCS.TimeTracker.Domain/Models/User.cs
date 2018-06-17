namespace TwoCS.TimeTracker.Domain.Models
{
    using System.Collections.Generic;

    public class User : ModelBase, IModel<string>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }
    }
}
