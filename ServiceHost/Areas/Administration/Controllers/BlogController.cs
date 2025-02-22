using Microsoft.AspNetCore.Mvc;
using Resume.Application.Dtos.Blog.ArticleCategory;
using Resume.Application.Services.Interface;
using Resume.Domain.Entities.Blog;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class BlogController : AdminBaseController
    {
        #region Fields

        private readonly IBlogService _blogService;



        #endregion


        #region Constructor

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        #endregion


        #region Article Category

        [HttpGet("article-category-list")]
        public async Task<IActionResult> FilterArticleCategory()
        {
            var category = await _blogService.GetAllArticleCategory();
            return View(category);
        }

        [HttpGet("create-article-category")]
        public  IActionResult CreateArticleCategory()
        {
            return View();
        }


        [HttpPost("create-article-category")]
        public async Task<IActionResult> CreateArticleCategory(CreateArticleCategoryDto command)
        {
            var result = await _blogService.CreateArticleCategory(command);

            return result switch
            {
                true => RedirectToAction("FilterArticleCategory", "Blog", new { area = "Administration" }),
                false => RedirectToAction("PageNotFound", "Home")
            };
        }

        #endregion
    }
}
