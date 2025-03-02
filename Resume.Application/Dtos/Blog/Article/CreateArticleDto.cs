using System.ComponentModel.DataAnnotations;

namespace Resume.Application.Dtos.Blog.Article
{
    public class CreateArticleDto
    {
        public long CategoryId { get; set; }

        [Display(Name = "عنوان مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ArticleTitle { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? Description { get; set; }

        [Display(Name = "تصویر مقاله")]
        [MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? ArticleImage { get; set; }

        public Domain.Entities.Blog.ArticleCategory ArticleCategory { get; set; }
    }
}
