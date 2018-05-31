using Microsoft.AspNetCore.Mvc;

namespace EMS.Web.Controllers
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

        public IActionResult DriveAndSlamEvent()
        {
            return View();
        }

        public IActionResult WestCoastAutoDeal()
        {
            return View();
        }
    }
}