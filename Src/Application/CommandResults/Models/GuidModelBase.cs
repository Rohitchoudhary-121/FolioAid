using System.ComponentModel.DataAnnotations;

namespace Application.CommandResults.Models
{
    public class GuidModelBase
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        protected GuidModelBase()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
