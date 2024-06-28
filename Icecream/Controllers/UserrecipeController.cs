using Icecream.Data;
using Icecream.Models;
using Microsoft.AspNetCore.Mvc;

namespace Icecream.Controllers
{
    public class UserrecipeController : Controller
    {

        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment enviroment;

        public UserrecipeController(ApplicationDbContext context, IWebHostEnvironment enviroment)

        {

            this.context = context;
            this.enviroment = enviroment;
        }
        

        public IActionResult userrecipe_index()
        {
            var data = context.Userrecipes.ToList();
            return View(data);
        }

        public IActionResult userrecipe_submit()
        {
            return View();
        }
        [HttpPost]

        // Submitting Recipe after recipe submit button get's clicked
        public IActionResult userrecipe_submit(Userrecipe model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniquefilename = UploadImage(model);
                    var data = new Userrecipe()
                    {
                        user_name = model.user_name,
                        user_email = model.user_email,
                        recipe_name = model.recipe_name,
                        recipe_description = model.recipe_description,
                        Path = uniquefilename
                    };
                    context.Userrecipes.Add(data);
                    context.SaveChanges();
                    TempData["success"] = "Your Recipe is submitted successfully!";
                    return RedirectToAction("userrecipe_submit");
                }
                else
                {
                    TempData["error"] = "Your Recipe is not submitted please check!";
                }
                ModelState.AddModelError("", "Model property is not valid please check");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return View(model);
        }

        private string UploadImage(Userrecipe model)
        {
            string uniquefilename = "";
            if (model.Image != null)
            {
                string UploadFolder = Path.Combine(enviroment.WebRootPath, "website/img");
                uniquefilename = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string FilePath = Path.Combine(UploadFolder, uniquefilename);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniquefilename;
        }


        public IActionResult userrecipe_detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var data = context.Userrecipes.Where(e => e.Id == id).SingleOrDefault();
            return View(data);

        }

        public IActionResult userrecipe_givepoints(int id)
        {
            var data = context.Userrecipes.Where(e => e.Id == id).SingleOrDefault();
            if (data != null)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("userrecipe_index");
            }
        }


        [HttpPost]
        public IActionResult Ed(Userrecipe model)
        {
            try
            {
                var existingUserRecipe = context.Userrecipes.Find(model.Id);

                if (existingUserRecipe == null)
                {
                    return NotFound();
                }

                // Manually validate the points property (you can add more validation logic if needed)
                if (model.points < 0)
                {
                    TempData["error"] = "Points must be a non-negative value.";
                    return RedirectToAction("userrecipe_index");
                }

                // Update only the points property
                existingUserRecipe.points = model.points;

                context.Userrecipes.Update(existingUserRecipe);
                context.SaveChanges();

                TempData["warning"] = "Points given successfully!";
                return RedirectToAction("userrecipe_index");
            }
            catch (Exception ex)
            {
                // Log the exception to a file or console
                Console.WriteLine($"Exception: {ex.Message}");
                TempData["error"] = $"Error updating points: {ex.Message}";
            }

            return RedirectToAction("userrecipe_index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var userRecipe = context.Userrecipes.Find(id);

                if (userRecipe == null)
                {
                    return NotFound();
                }

                context.Userrecipes.Remove(userRecipe);
                context.SaveChanges();

                TempData["error"] = "User Recipe deleted successfully!";
            }
            catch (Exception ex)
            {
                // Log the exception to a file or console
                Console.WriteLine($"Exception: {ex.Message}");
                TempData["error"] = $"Error deleting recipe: {ex.Message}";
            }

            return RedirectToAction("userrecipe_index");
        }

    }
}
