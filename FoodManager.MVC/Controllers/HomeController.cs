using FoodManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodManager.MVC.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        /// <summary>
        /// Displays the home page.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the no access page when a user tries to access restricted areas.
        /// </summary>
        public IActionResult NoAccess()
        {
            return View();
        }

        /// <summary>
        /// Displays the error page with relevant error information.
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
