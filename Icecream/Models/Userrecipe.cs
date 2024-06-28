using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Icecream.Models
{
    public class Userrecipe
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string? user_name { get; set; }
        [Required]
        public string? user_email { get; set; }
        [Required]
        public string? recipe_name { get; set; }
        [Required]
        public string? recipe_description { get; set; }
        

        [DisplayName("Image")]

        public string? Path { get; set; }

        [NotMapped]
        [DisplayName("Choose image")]
        public IFormFile Image { get; set; }

        
        public int points { get; set; } = 0;
    }
}
