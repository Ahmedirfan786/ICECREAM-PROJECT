using Icecream.Data;
using Icecream.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Icecream.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {

        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment enviroment;

        private readonly UserManager<ApplicationUser> _manager;

        public DashboardController(ApplicationDbContext context, IWebHostEnvironment enviroment, UserManager<ApplicationUser>_userManager)

        {

            this.context = context;
            this.enviroment = enviroment;

            _manager = _userManager;
        }

        public IActionResult dashboard_index()
        {
            return View();
        }
        public IActionResult dashboard_404()
        {
            return View();
        }
        public IActionResult dashboard_blank()
        {
            return View();
        }
        public IActionResult dashboard_button()
        {
            return View();
        }
        public IActionResult dashboard_chart()
        {
            return View();
        }
        public IActionResult dashboard_element()
        {
            return View();
        }
        public IActionResult dashboard_form()
        {
            return View();
        }
        public IActionResult dashboard_signin()
        {
            return View();
        }
        public IActionResult dashboard_signup()
        {
            return View();
        }
        public IActionResult dashboard_table()
        {
            return View();
        }
        public IActionResult dashboard_typography()
        {
            return View();
        }
        public IActionResult dashboard_widget()
        {
            return View();
        }

        public IActionResult feedback_index()
        {
            var data = context.Feedbacks.ToList();
            return View(data);
        }
        public IActionResult user_index()
        {
            var nonAdminUsers = _manager.Users.Where(user => user.Email != "admin@admin.com");
            return View(nonAdminUsers);
        }

        public IActionResult message_index()
        {
            var data=context.Contacts.ToList();
            return View(data);
        }
    }
}
