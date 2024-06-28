using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Icecream.Data;
using Icecream.Models;
using Icecream.Data;
using Microsoft.AspNetCore.Authorization;

namespace Icecream.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment enviroment;

        public BookController(ApplicationDbContext context, IWebHostEnvironment enviroment)

        {

            this.context = context;
            this.enviroment = enviroment;
        }
        public IActionResult book_index()
        {
            var data = context.Books.ToList();
            return View(data);
        }
        public IActionResult book_create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Book model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniquefilename = UploadImage(model);
                    var data = new Book()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Price = model.Price,

                        Path = uniquefilename
                    };
                    TempData["success"] = "Book added successfully!";
                    context.Books.Add(data);
                    context.SaveChanges();
                    return RedirectToAction("book_index");
                }
                else
                {
                    TempData["error"] = "The Book is not created!";
                    return RedirectToAction("book_index");
                }
                ModelState.AddModelError("", "Model property is not valid please check");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return View(model);
        }

        private string UploadImage(Book model)
        {
            string uniquefilename = "";
            if (model.Image != null)
            {
                string UploadFolder = Path.Combine(enviroment.WebRootPath, "Website/img");
                uniquefilename = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string FilePath = Path.Combine(UploadFolder, uniquefilename);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniquefilename;
        }
        public IActionResult book_delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var data = context.Books.Where(e => e.Id == id).SingleOrDefault();
                if (data != null)
                {
                    string deleteFromFolder = Path.Combine(enviroment.WebRootPath, "Website/img");
                    string currentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFromFolder, data.Path);
                    if (currentImage != null)
                    {
                        if (System.IO.File.Exists(currentImage))
                        {
                            System.IO.File.Delete(currentImage);
                        }
                    }
                    TempData["error"] = "The Book is deleted successfully!";
                    context.Books.Remove(data);
                    context.SaveChanges();
                    //TempData["Success"] = "Record Deleted!";
                }
            }
            return RedirectToAction("book_index");
        }
        public IActionResult book_detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var data = context.Books.Where(e => e.Id == id).SingleOrDefault();
            return View(data);

        }
        public IActionResult book_edit(int id)
        {
            var data = context.Books.Where(e => e.Id == id).SingleOrDefault();
            if (data != null)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("book_index");
            }
        }
        [HttpPost]
        public IActionResult Edit(Book model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = context.Books.Where(e => e.Id == model.Id).SingleOrDefault();
                    string uniqueFileName = string.Empty;

                    if (model.Image != null)
                    {
                        if (data.Path != null)
                        {
                            string filepath = Path.Combine(enviroment.WebRootPath, "/Website/img", data.Path);
                            if (System.IO.File.Exists(filepath))
                            {
                                System.IO.File.Delete(filepath);
                            }
                        }
                        uniqueFileName = UploadImage(model);
                    }

                    data.Name = model.Name;
                    data.Description = model.Description;
                    data.Price = model.Price;

                    if (model.Image != null)
                    {
                        data.Path = uniqueFileName;
                    }

                    TempData["warning"] = "The Book is updated successfully!";
                    context.Books.Update(data);
                    context.SaveChanges();
                }
                else
                {
                    TempData["error"] = "The Book is not updated!";
                    TempData["bookNotEdited"] = "true"; // Added line to indicate book didn't edit
                    return RedirectToAction("book_index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction("book_index");
        }

    }
}
