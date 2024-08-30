using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
	public class Login
	{

		[Required(ErrorMessage = "Введите Email")]
		[EmailAddress(ErrorMessage = "Некорректный электронный адрес")]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Введите пароль")]
		public string Password { get; set; }

		public bool RememberUser { get; set; }

		public string? ReturnUrl { get; set; }	

     
    }
}
