using Icecream.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Icecream.Models;


namespace Icecream.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Recipe> Recipes { get; set; }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }

        public virtual DbSet<FAQ> FAQs { get; set; }

        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<Userrecipe> Userrecipes { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Userfaq> Userfaqs { get; set; }

    }
}
