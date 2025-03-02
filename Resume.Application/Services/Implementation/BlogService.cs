using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Dtos.Blog.Article;
using Resume.Application.Dtos.Blog.ArticleCategory;
using Resume.Application.Dtos.Project;
using Resume.Application.Services.Interface;
using Resume.Application.Utilities;
using Resume.Domain.Entities.Blog;
using Resume.Domain.Entities.Project;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation
{
    public class BlogService : IBlogService
    {
        #region Fields

        private readonly IGenericRepository<ArticleCategory> _articleCategoryRepository;
        private readonly IGenericRepository<Article> _articleRepository;



        #endregion

        #region Constructor

        public BlogService(IGenericRepository<ArticleCategory> articleCategoryRepository, IGenericRepository<Article> articleRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _articleRepository = articleRepository;
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
                    ShortDescription = x.ShortDescription,
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

        public async Task<List<ArticleCategoryListDto>> GetAllCategories()
        {
            return await _articleCategoryRepository
                .GetQuery()
                .AsQueryable()
                .Select(x => new ArticleCategoryListDto()
                {
                    CategoryId = x.Id,
                    ArticleCategoryTitle = x.ArticleCategoryTitle
                }).ToListAsync();
        }

        #endregion


        #region Article

        public async Task<List<FilterArticleDto>> GetAllArticle()
        {
            return await _articleRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Include(x=>x.ArticleCategory)
                .Select(x => new FilterArticleDto
                {
                    Id = x.Id,
                    ArticleCategoryTitle = x.ArticleCategory.ArticleCategoryTitle,
                    ArticleTitle = x.ArticleTitle,
                    ArticleImage = x.ArticleImage,
                    ShortDescription = x.Description,
                    Description = x.Description,
                    CreateDate = x.CreateDate.ToStringShamsiDate()

                }).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<bool> CreateArticle(CreateArticleDto command, IFormFile articleImage)
        {

            var uploadFolder = Path.Combine("wwwroot/uploads");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(articleImage.FileName);
            var filePath = Path.Combine("wwwroot/uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await articleImage.CopyToAsync(stream);

                var newArticle = new Article
                {
                    ArticleCategoryId = command.CategoryId,
                    ArticleTitle = command.ArticleTitle,
                    ArticleImage = fileName,
                    ShortDescription = command.ShortDescription,
                    Description = command.Description,
                };

                await _articleRepository.AddEntity(newArticle);
                await _articleRepository.SaveChanges();

            }
            
            return true;
        }

        #endregion













        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_articleCategoryRepository != null)
            {
                await _articleCategoryRepository.DisposeAsync();
            }

            if (_articleRepository != null)
            {
                await _articleRepository.DisposeAsync();
            }
        }

        #endregion


    }
}
