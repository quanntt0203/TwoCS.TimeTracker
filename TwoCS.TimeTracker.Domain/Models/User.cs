namespace TwoCS.TimeTracker.Domain.Models
{
    using System.Collections.Generic;

    public class User : ModelBase, IModel<string>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string ManagerId { get; set; }

        public virtual User Magager { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IEnumerable<User> AssignedMembers { get; set; }

        public IEnumerable<Project> AssignedProjects { get; set; }
    }
}
