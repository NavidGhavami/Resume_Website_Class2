namespace Resume.Application.Dtos.Blog.ArticleCategory
{
    public class FilterArticleCategoryDto
    {
        public long Id { get; set; }
        public string ArticleCategoryTitle { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ArticleCategoryImage { get; set; }
        public string CreateDate { get; set; }

    }
}
