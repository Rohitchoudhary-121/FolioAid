using Domain.Entity.EntityHelper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class ProjectIndustryMapping : GuidModelBase
    {
        [Required]
        public string ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }

        [Required]
        public string IndustryId { get; set; }

        [ForeignKey(nameof(IndustryId))]
        public virtual Industry Industry { get; set; }
    }
}
