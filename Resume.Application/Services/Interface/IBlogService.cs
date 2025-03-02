using Microsoft.AspNetCore.Http;
using Resume.Application.Dtos.Blog.Article;
using Resume.Application.Dtos.Blog.ArticleCategory;
using Resume.Domain.Entities.Blog;

namespace Resume.Application.Services.Interface;

public interface IBlogService : IAsyncDisposable
{
    #region Article Category

    Task<List<FilterArticleCategoryDto>> GetAllArticleCategory();
    Task<bool> CreateArticleCategory(CreateArticleCategoryDto command);
    Task<List<ArticleCategoryListDto>> GetAllCategories();

    #endregion


    #region Article

    Task<List<FilterArticleDto>> GetAllArticle();
    Task<bool> CreateArticle(CreateArticleDto command, IFormFile articleImage);

    #endregion
}