using Icecream.Data;
using Icecream.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Icecream.Controllers
{
	public class WebsiteController : Controller
	{
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment enviroment;

        public WebsiteController(ApplicationDbContext context, IWebHostEnvironment enviroment)

        {

            this.context = context;
            this.enviroment = enviroment;
        }

        public IActionResult website_index()
        {
            try
            {
                var topUsers = context.Userrecipes
                    .Where(u => u.points > 0)  // Exclude users with points equal to 0
                    .GroupBy(u => u.user_name)
                    .Select(g => new
                    {
                        UserName = g.Key,
                        TotalPoints = g.Sum(r => r.points)
                    })
                    .OrderByDescending(u => u.TotalPoints)
                    .Take(3)
                    .ToList();

                var topUserRecipes = new List<Userrecipe>();

                foreach (var user in topUsers)
                {
                    var userRecipe = context.Userrecipes
                        .Where(u => u.user_name == user.UserName && u.points > 0)
                        .OrderByDescending(r => r.points)
                        .FirstOrDefault();

                    if (userRecipe != null)
                    {
                        topUserRecipes.Add(userRecipe);
                    }
                }

                return View(topUserRecipes);
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or return an error view
                Console.WriteLine($"Exception: {ex.Message}");
                TempData["error"] = $"Error fetching top users: {ex.Message}";
                return View();
            }
        }


        public IActionResult website_about()
        {
            return View();
        }


        public IActionResult website_contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact model)
        {
            if (ModelState.IsValid)
            {
                context.Add(model);
                context.SaveChanges();
                TempData["success"] = "Your Message is send successfully!";
                return RedirectToAction("website_index");

            }
            else
            {
                TempData["error"] = "The Message is not send kindly check";

            }
            return View();
        }



        public IActionResult website_gallery()
        {
            return View();
        }
        public IActionResult website_service()
        {
            return View();
        }
        public IActionResult website_recipes()
        {
            var data = context.Recipes.ToList();
            return View(data);
        }

        public IActionResult website_recipedetail(int id)
        {
            //if (id == 0)
            //{
            //    return NotFound();
            //}
            var data = context.Recipes.Where(e => e.Id == id).SingleOrDefault();
            return View(data);
        }



        public IActionResult website_recipesubmit()
        {
            return View();
        }






        public IActionResult website_books()
        {
            var data = context.Books.ToList();
            return View(data);
        }

        public IActionResult website_bookdetail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var data = context.Books.Where(e => e.Id == id).SingleOrDefault();
            return View(data);

        }

        public IActionResult website_faq()
        {
            var data=context.FAQs.ToList();
            return View(data);
        }

      



        public IActionResult website_userfaq()
        {
            return View();
        }
        [HttpPost]
        public IActionResult userfaqsubmit(Userfaq model)
        {
            if (ModelState.IsValid)
            {
                context.Userfaqs.Add(model);
                context.SaveChanges();
                TempData["success"] = "The FAQ is Submitted successfully!";
            }
            return RedirectToAction("website_faq");
        }




        public IActionResult website_feedback()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Feedback(Feedback model)
        {
            if (ModelState.IsValid)
            {
                context.Add(model);
                context.SaveChanges();
                TempData["success"] = "Your feedback was sent successfully.";
                return RedirectToAction("website_index");
            }
            else
            {
                TempData["error"] = "Your feedback was not sent. Please check the form and try again.";
                return RedirectToAction("website_index");
            }
        }


        public IActionResult website_orderform(int id)
        {
            var data = context.Books.Where(e => e.Id == id).SingleOrDefault();
            return View(data);
        }


        [HttpPost]
        public IActionResult Placeorder(Order order)
        {
            if (ModelState.IsValid)
            {
               
                context.Orders.Add(order);
                context.SaveChanges();

                TempData["success"] = "Transcation Successful Your Order has been Placed !";

                return RedirectToAction("website_books");
            }

            TempData["error"] = "Transcation Unsuccessful Your Order has not been Placed !";

            return View("website_index");
        }

        

        

    }
}
