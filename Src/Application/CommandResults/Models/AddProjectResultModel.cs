using System.ComponentModel.DataAnnotations;

namespace Application.CommandResults.Models
{
    public class AddProjectResultModel
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

    }
}
