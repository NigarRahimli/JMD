using Microsoft.AspNetCore.Mvc;

namespace JMD.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
