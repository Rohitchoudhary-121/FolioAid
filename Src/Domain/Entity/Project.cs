using Domain.Entity.EntityHelper;
using Domain.Entity.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Project : GuidModelBase, IRecordCreated, ISoftDeletable
    {
        [MaxLength(150)]
        [Required]
        public string Title { get; set; }
        public bool Status { get; set; }
        public string? Summary { get; set; }
        public string? Challenges { get; set; }
        public string? Solutions { get; set; }
        public string? BannerImage { get; set; }
        public string? PublicURL { get; set; }
        public string? DemoURL { get; set; }
        public string? AndroidURL { get; set; }
        public string? AppleURL { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Soft Deletes Entity
        /// </summary>
        public void Delete() => OnDelete();

        /// <summary>
        /// Bring Back Entity to Life
        /// </summary>
        public void BringBack() => OnBringBack();

        /// <summary>
        /// Logic for deleting entity (Ex: Cascade deletion)
        /// </summary>
        protected virtual void OnDelete()
            => IsDeleted = true;

        /// <summary>
        /// Bring Back Entity Logic
        /// </summary>
        protected virtual void OnBringBack()
            => IsDeleted = false;


        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<ProjectIndustryMapping> ProjectIndustryMapping { get; set; }

        public virtual ICollection<ProjectTechnologyMapping> ProjectTechnologyMapping { get; set; }
    }
}
