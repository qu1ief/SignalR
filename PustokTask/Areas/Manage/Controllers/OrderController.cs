using Microsoft.AspNetCore.Mvc;
using PustokTask.Data;

namespace PustokTask.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class OrderController(PustokDbContex dbContex) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
