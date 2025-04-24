using Microsoft.AspNetCore.Mvc;

namespace OSS_Main.Controllers
{
    public class GeminiViewController : Controller
    {
        public IActionResult Ask()
        {
            return View();
        }
    }
}
