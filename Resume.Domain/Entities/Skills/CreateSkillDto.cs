using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Skills
{
    public class CreateSkillDto
    {
        #region Properties

        #region Properties



        [Display(Name = "عنوان مهارت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string SkillTitle { get; set; }


        [Display(Name = "درصد توانایی مهارت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string SkillPercent { get; set; }

        #endregion

        #endregion


        public enum CreateSkillResult
        {
            Success,
            Error
        }
    }
}
