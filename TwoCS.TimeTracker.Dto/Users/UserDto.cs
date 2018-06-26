namespace TwoCS.TimeTracker.Dto
{
    using System.Collections.Generic;

    public class UserDto : DtoBase
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IEnumerable<UserDto> AssignedMembers { get; set; }

        public IEnumerable<ProjectDto> AssignedProjects { get; set; }

        public UserDto Manager { get; set; }
    }
}
