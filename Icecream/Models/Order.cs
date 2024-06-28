using System.ComponentModel.DataAnnotations;

namespace Icecream.Models
{
    public class Order
    {
        [Key]
        public int? Id { get; set; }

        [Required]
      public string? UserName  { get; set; }

       [Required]
      public  string?  UserEmail {get;set;}

       [Required]
        public string? UserPhone {get;set;}

        [Required]
        public string? UserAdress { get; set; }

        [Required]
       public string?  UserCreditDebit{ get; set; }

        [Required]
       public string?  BookName  {get;set;}

       [Required]
       public int? BookPrice  { get; set; }

        [Required]
        public string? BookingPath { get; set; }


        public string Status { get; set; } = "Pending";
    }
}
