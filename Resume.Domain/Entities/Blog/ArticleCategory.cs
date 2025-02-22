using Resume.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Blog
{
    public class ArticleCategory : BaseEntity
    {
        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ArticleCategoryTitle { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? Description { get; set; }

        [Display(Name = "تصویر")]
        [MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? ArticleCategoryImage { get; set; }

        public ICollection<Article> Articles { get; set; }

    }
}
