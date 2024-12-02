using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult NotFoundPage()
        {
            return View();
        }
    }
}
