using Icecream.Data;
using Icecream.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Icecream.Controllers
{

    [Authorize(Roles = "Admin")]
    public class FAQController : Controller

    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment enviroment;

        public FAQController(ApplicationDbContext context, IWebHostEnvironment enviroment)

        {

            this.context = context;
            this.enviroment = enviroment;
        }
        public IActionResult faq_index()
        {
            var data = context.FAQs.ToList();

            return View(data);
        }
        public IActionResult faq_create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(FAQ model)
        {
            if (ModelState.IsValid)
            {
                context.FAQs.Add(model);
                context.SaveChanges();
                TempData["success"] = "The FAQ is created successfully!";
            }
            return RedirectToAction("faq_index");
        }
        public IActionResult faq_edit(int id)
        {
            var data = context.FAQs.Where(e => e.Id == id).SingleOrDefault();
            if (data != null)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("faq_index"); 
            }
        }
        [HttpPost]
        public IActionResult Edit(FAQ model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.FAQs.Update(model);
                    context.SaveChanges();
                    TempData["warning"] = "The FAQ is updated successfully!";
                }
                else {
                    return RedirectToAction("faq_index");
                }
            }catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
            }
            return RedirectToAction("faq_index");
        }
        public IActionResult faq_details(int id)
        {
            var data = context.FAQs.Where(e => e.Id == id).SingleOrDefault();
            if (data != null)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("faq_index");
            }
        }
        public IActionResult faq_delete(int id)
        {
            var data = context.FAQs.Where(e => e.Id == id).SingleOrDefault();
            if (data != null)
            {
                context.Remove(data);
                context.SaveChanges();
                TempData["error"] = "The FAQ is deleted successfully!";
                return RedirectToAction("faq_index");
            }
            else
            {
                return NotFound();
            }
        }


        public IActionResult userfaqs_list()
        {
            var data = context.Userfaqs.ToList();
            return View(data);
        }


    }
}
