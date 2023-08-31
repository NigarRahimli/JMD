using Microsoft.AspNetCore.Mvc;

namespace JMD.Areas.Dashboard.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
