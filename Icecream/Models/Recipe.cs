using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Icecream.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Recipe Name")]
        [Required]
        public string? Name { get; set; }


        [DisplayName("Description")]
        [Required]
        public string? Description { get; set; }

        [DisplayName("Status")]
        [Required]
        public string? Status { get; set; }

        [DisplayName("Image")]

        public string? Path { get; set; }

        [NotMapped]
        [DisplayName("Choose image")]
        public IFormFile Image { get; set; }
    }
}
