using Microsoft.AspNetCore.Mvc;
using Resume.Application.Dtos.Contact;
using Resume.Application.Services.Interface;

namespace ServiceHost.Controllers
{
    public class HomeController : Controller
    {

        private readonly IContactService _contactService;

        public HomeController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("404-page-not-found")]
        public IActionResult PageNotFound()
        {
            return View();
        }


        [HttpGet("contact-me")]
        public IActionResult CreateNewMessage()
        {
            return View();
        }

        [HttpPost("contact-me")]
        public async Task<IActionResult> CreateNewMessage(CreateContactDto command)
        {
            var result = await _contactService.SendNewMessage(command);

            switch (result)
            {
                case CreateContactResult.Success:
                    return RedirectToAction("Index", "Home");
                case CreateContactResult.Error:
                    break;
            }

            return View();
        }
    }
}
