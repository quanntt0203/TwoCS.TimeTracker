namespace TwoCS.TimeTracker.Dto
{
    using System.Collections.Generic;

    public class UserDto : DtoBase
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }
    }
}
