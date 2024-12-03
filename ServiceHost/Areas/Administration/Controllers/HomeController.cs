using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class HomeController : AdminBaseController
    {
        
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet("404-page-not-found")]
        //public ActionResult NotFoundPage()
        //{
        //    return View();
        //}
    }
}
