using System.ComponentModel.DataAnnotations;

namespace Icecream.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? feedback { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "The value must be between 1 and 5.")]
        public int? Rating { get; set; }
    }
}
