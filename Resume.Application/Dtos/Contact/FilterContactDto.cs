using System.ComponentModel.DataAnnotations;

namespace Resume.Application.Dtos.Contact
{
	public class FilterContactDto
	{
		public long Id { get; set; }
		public string Fullname { get; set; }
		public string? Email { get; set; }
		public string Message { get; set; }
		public string CreateDate { get; set; }
	}
}
