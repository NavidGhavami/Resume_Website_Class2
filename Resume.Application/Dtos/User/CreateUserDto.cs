﻿using System.ComponentModel.DataAnnotations;

namespace Resume.Application.Dtos.User
{
    public class CreateUserDto
    {
		#region Properties

		[Display(Name = "نام و نام خانوادگی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string Fullname { get; set; }

		[Display(Name = "ایمیل")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		[EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }

		[Display(Name = "موبایل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string Mobile { get; set; }

		[Display(Name = "تاریخ تولد")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string BirthDate { get; set; }

		[Display(Name = "محل تولد")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string BirthPlace { get; set; }

		[Display(Name = "توضیحات")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string Description { get; set; }

		[Display(Name = "کلمه ی عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[DataType(DataType.Password)]
		[MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string Password { get; set; }

		[Display(Name = " تکرار کلمه ی عبور")]
		[Required(ErrorMessage = "لطفا تکرار {0} را وارد کنید")]
		[MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		[Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
		public string ConfirmPassword { get; set; }

		public string PasswordSalt { get; set; }

		[Display(Name = " بلاک شده / نشده")]
		public bool IsBlock { get; set; }

		[Display(Name = " تصویر آواتار")]
		public string? Avatar { get; set; }

		#endregion
	}

    public enum CreateUserResult
    {
        Success,
        Error,
        MobileExist
    }
}
