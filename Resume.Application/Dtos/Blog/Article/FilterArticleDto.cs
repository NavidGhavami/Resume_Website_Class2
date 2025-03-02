namespace Resume.Application.Dtos.Blog.Article
{
    public class FilterArticleDto
    {
        public long Id { get; set; }
        public string ArticleCategoryTitle { get; set; }
        public string ArticleTitle { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ArticleImage { get; set; }
        public string CreateDate { get; set; }

    }
}
