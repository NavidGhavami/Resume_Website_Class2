using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface;

namespace ServiceHost.Controllers
{
    public class BlogController : SiteBaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("article-list")]
        public async Task<IActionResult> FilterArticle()
        {
            var article = await _blogService.GetAllArticle();
            return View(article);
        }
    }
}
