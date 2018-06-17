
namespace TwoCS.TimeTracker.Domain.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Project : ModelBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public int OrderNo { get; set; }
    }
}
