namespace TwoCS.TimeTracker.Dto
{
    using System.Collections.Generic;

    public class DtoBase : IDto
    {
        public bool IsValid { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
