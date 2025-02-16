using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface;

namespace ServiceHost.Areas.Administration.Controllers
{
	public class HomeController : AdminBaseController
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

        [HttpGet("message-list")]
        public async Task<IActionResult> GetAllMessages()
        {
            var message = await _contactService.GetAllMessages();
                return View(message);
        }



	}
}
