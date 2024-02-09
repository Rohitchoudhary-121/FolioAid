using Domain.Entity.EntityHelper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class ProjectImageGallery : GuidModelBase
    {
        [Required]
        public string ProjectId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }
        public string? Tittle { get; set; }
        public string? ImagePath { get; set; }

    }
}
