using System.ComponentModel.DataAnnotations;

namespace Resume.Application.Dtos.User
{
	public class UserLoginDto
	{
		[Display(Name = "موبایل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string Mobile { get; set; }

		[Display(Name = "کلمه ی عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[DataType(DataType.Password)]
		[MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string Password { get; set; }
	}

	public enum UserLoginResult
	{
		Success,
		Error,
		UserNotFound
	}
}
