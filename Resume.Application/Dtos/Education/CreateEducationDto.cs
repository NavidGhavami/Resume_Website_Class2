using System.ComponentModel.DataAnnotations;

namespace Resume.Application.Dtos.Education
{
    public class CreateEducationDto
    {
        #region Properties

        [Display(Name = "نام دانشگاه/دانشکده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string UniversityName { get; set; }

        [Display(Name = "سال تحصیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string EducationYear { get; set; }


        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }


        #endregion
    }

    public enum CreateEducationResult
    {
        Success,
        Error
    }
}
