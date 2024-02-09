using Domain.Entity.EntityHelper;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Industry : GuidModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
