using System.ComponentModel.DataAnnotations;

namespace BlazorWebApp.Models
{
    public class BlogPost
    {
        [Required]
        [MinLength(5)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Text { get; set; } = string.Empty;
    }
}
