using Icecream.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Icecream.Data
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Subscription { get; set; }
        [Required]

        public string? Cardnumber { get; set; }
    }
}
