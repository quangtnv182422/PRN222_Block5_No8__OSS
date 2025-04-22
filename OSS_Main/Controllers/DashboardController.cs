using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OSS_Main.Controllers
{
    [Authorize(Roles = "Admin, Sales")]
    public class DashboardController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
