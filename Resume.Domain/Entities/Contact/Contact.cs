using Resume.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Contact
{
	public class Contact : BaseEntity
	{
		#region Properties

		[Display(Name = "آدرس نقشه")]
		public string? MapAddress { get; set; }

		[Display(Name = "نام و نام خانوادگی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string Fullname { get; set; }

		[Display(Name = "نام دانشگاه/دانشکده")]
		[EmailAddress(ErrorMessage = "فرمت ایمیل نادرست می باشد")]
		[MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string? Email { get; set; }

		[Display(Name = "پیغام")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(550, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string Message { get; set; }

		#endregion
	}
}
