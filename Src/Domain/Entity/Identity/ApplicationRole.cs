using Microsoft.AspNetCore.Identity;

namespace Domain.Entity.Identity
{
    public partial class ApplicationRole : IdentityRole<string>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
