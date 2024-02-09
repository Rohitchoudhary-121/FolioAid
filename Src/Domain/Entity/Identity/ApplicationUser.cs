using Domain.Entity.EntityHelper;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.Identity
{
    public class ApplicationUser : IdentityUser, IRecordCreated, ISoftDeletable
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
        [MaxLength(255)]
        public string? ActivationString { get; set; }
        [MaxLength(36)]
        public string? PortfolioString { get; set; }
        public DateTime? ActivationStringExpiryDate { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
       

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

    }
}
