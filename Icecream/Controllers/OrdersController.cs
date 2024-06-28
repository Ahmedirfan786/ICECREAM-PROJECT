using Icecream.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Icecream.Controllers
{
    public class OrdersController : Controller
    {
    private readonly ApplicationDbContext context;
        public OrdersController(ApplicationDbContext context)

        {

            this.context = context;
        }
        public IActionResult CompleteOrders(Model model)
        {
            var data = context.Orders.Where(s => s.Status == "completed");
            return View(data.ToList());
        }
        public IActionResult PendingOrders(Model model)
        {
            var data = context.Orders.Where(s => s.Status == "pending");
            return View(data.ToList());
        }

        [HttpPost]
        public IActionResult MarkAsComplete(int id)
        {
            var order = context.Orders.Find(id);

            if (order != null && order.Status == "Pending")
            {
                order.Status = "Completed";
                context.SaveChanges();
                TempData["success"] = "Order status changed to Completed!";
            }

            return RedirectToAction("PendingOrders");
        }
    }
}
