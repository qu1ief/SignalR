using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PustokTask.Hubs;
using PustokTask.Models;

namespace PustokTask.Controllers
{
    public class ChatController(
        UserManager<AppUser> userManager,
        IHubContext<ChatHub> hub
        ) : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Users = userManager.Users.ToList();
            return View();
        }

        public IActionResult SendUser(string id)
        {
            var user =userManager.FindByIdAsync(id).Result;
            hub.Clients.Client(user.ConnectionId).SendAsync("sendMessageSpecUser", id);

            return Content("sended...");
        }
    }
}
