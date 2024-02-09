using Domain.Entity.EntityHelper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class ProjectTechnologyMapping : GuidModelBase
    {
        [Required]
        public string ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }

        [Required]
        public string TechnologyId { get; set; }

        [ForeignKey(nameof(TechnologyId))]
        public virtual Technology Technology { get; set; }


    }
}
