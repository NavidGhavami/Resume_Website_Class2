using Microsoft.EntityFrameworkCore;
using Resume.Application.Dtos.Blog.ArticleCategory;
using Resume.Application.Services.Interface;
using Resume.Application.Utilities;
using Resume.Domain.Entities.Blog;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation
{
    public class BlogService : IBlogService
    {
        #region Fields

        private readonly IGenericRepository<ArticleCategory> _articleCategoryRepository;



        #endregion

        #region Constructor

        public BlogService(IGenericRepository<ArticleCategory> articleCategoryRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
        }

        #endregion


        #region Article Category

        public async Task<List<FilterArticleCategoryDto>> GetAllArticleCategory()
        {
            return await _articleCategoryRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(x => new FilterArticleCategoryDto
                {
                    Id = x.Id,
                    ArticleCategoryTitle = x.ArticleCategoryTitle,
                    ArticleCategoryImage = null,
                    ShortDescription = x.Description,
                    Description = x.Description,
                    CreateDate = x.CreateDate.ToStringShamsiDate()

                }).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<bool> CreateArticleCategory(CreateArticleCategoryDto command)
        {
            var newCategory = new ArticleCategory
            {
                ArticleCategoryTitle = command.ArticleCategoryTitle,
                ArticleCategoryImage = null,
                ShortDescription = command.ShortDescription,
                Description = command.Description,
                
            };

            await _articleCategoryRepository.AddEntity(newCategory);
            await _articleCategoryRepository.SaveChanges();

            return true;
        }

        #endregion













        public async ValueTask DisposeAsync()
        {
            if (_articleCategoryRepository != null)
            {
                await _articleCategoryRepository.DisposeAsync();
            }
        }

        
    }
}
