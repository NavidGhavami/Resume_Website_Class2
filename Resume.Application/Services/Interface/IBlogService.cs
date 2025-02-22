using Resume.Application.Dtos.Blog.ArticleCategory;

namespace Resume.Application.Services.Interface;

public interface IBlogService : IAsyncDisposable
{
    #region Article Category

    Task<List<FilterArticleCategoryDto>> GetAllArticleCategory();
    Task<bool> CreateArticleCategory(CreateArticleCategoryDto command);

    #endregion


    #region Article



    #endregion
}