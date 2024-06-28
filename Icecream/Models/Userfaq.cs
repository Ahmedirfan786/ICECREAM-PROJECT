using System.ComponentModel.DataAnnotations;

namespace Icecream.Models
{
    public class Userfaq
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Question { get; set; }
    }
}
