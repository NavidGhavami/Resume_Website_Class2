using Resume.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Project
{
    public class Project : BaseEntity
    {
        #region properties


        [Display(Name = "عنوان پروژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ProjectTitle { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(120, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ProjectImage { get; set; }

        #endregion


        #region Constructor

        public Project(string projectTitle, string description, string projectImage)
        {
            ProjectTitle = projectTitle;
            Description = description;
            ProjectImage = projectImage;
        }

        #endregion
    }
}
