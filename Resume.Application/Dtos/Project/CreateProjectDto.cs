using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Resume.Application.Dtos.Project
{
    public class CreateProjectDto
    {
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
    }


    public enum CreateProjectResult
    {
        Success,
        Error
    }
}
