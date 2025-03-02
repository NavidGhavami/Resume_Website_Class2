using Microsoft.AspNetCore.Mvc;
using Resume.Application.Dtos.Blog.Article;
using Resume.Application.Dtos.Blog.ArticleCategory;
using Resume.Application.Services.Interface;

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

        #region Article

        [HttpGet("article-list")]
        public async Task<IActionResult> FilterArticle()
        {
            var article = await _blogService.GetAllArticle();
            ViewBag.Category = await _blogService.GetAllArticleCategory();

            return View(article);
        }

        [HttpGet("create-article")]
        public async Task<IActionResult> CreateArticle()
        {
            var articleCategory = await _blogService.GetAllCategories();
            ViewBag.Category = articleCategory;

            return View();
        }

        [HttpPost("create-article"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArticle(CreateArticleDto command, IFormFile articleImage)
        {
            var result = await _blogService.CreateArticle(command, articleImage);
            var categories = await _blogService.GetAllArticleCategory();
            ViewBag.Category = await _blogService.GetAllArticleCategory();

            return result switch
            {
                true => RedirectToAction("FilterArticle", "Blog", new { area = "Administration" }),
                false => RedirectToAction("PageNotFound", "Home")
            };

        }

        #endregion
    }
}
