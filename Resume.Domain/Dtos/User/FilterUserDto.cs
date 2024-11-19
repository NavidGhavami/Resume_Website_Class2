using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.User
{
    public class FilterUserDto
    {
        #region Properties

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Fullname { get; set; }

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Mobile { get; set; }

        [Display(Name = " بلاک شده / نشده")]
        public bool IsBlock { get; set; }

        [Display(Name = " تصویر آواتار")]
        public string? Avatar { get; set; }

        #endregion
    }
}
