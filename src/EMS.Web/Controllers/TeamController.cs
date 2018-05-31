using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Web.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}