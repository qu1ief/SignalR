using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PustokTask.Data;
using PustokTask.Hubs;

namespace PustokTask.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class OrderController(PustokDbContex dbContex,
       IHubContext<ChatHub> hubContext ) : Controller
    {
        public IActionResult Index()
        {
            return View(dbContex.Orders.Include(a=>a.AppUser).ToList());
        }

        public IActionResult Accept(int id)
        {
            var order = dbContex.Orders.Include(o=>o.AppUser).FirstOrDefault(o => o.Id == id);
            order.Status = Models.OrderStatus.Accepted;

            dbContex.SaveChanges();
            hubContext.Clients.Client(order.AppUser.ConnectionId).SendAsync("OrderAccepted");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Reject(int id)
        {
            var order = dbContex.Orders.FirstOrDefault(o => o.Id == id);
            order.Status = Models.OrderStatus.Rejected;

            dbContex.SaveChanges();
            hubContext.Clients.Client(order.AppUser.ConnectionId).SendAsync("OrderRejected");

            return RedirectToAction(nameof(Index));
        }
    }
}
