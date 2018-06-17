namespace TwoCS.TimeTracker.Domain.Models
{
    using System;

    public class Location : ModelBase
    {
        public DateTime CapturedDate { get; set; }

        public string CapturedInfo { get; set; }
    }
}
