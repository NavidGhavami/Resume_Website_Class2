using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    public class EducationController : Controller
    {
        public IActionResult Resume()
        {
            return View();
        }
    }
}
