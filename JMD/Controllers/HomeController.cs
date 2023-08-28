using JMD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JMD.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

      
    }
}