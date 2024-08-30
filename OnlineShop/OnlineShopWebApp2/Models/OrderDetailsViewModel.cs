using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
	public class OrderDetailsViewModel
	{

		[Required(ErrorMessage = "Вы не указали адрес")]
		public string UserAdress { get; set; }

		[Required(ErrorMessage = "Вы не указали имя")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Вы не указали номер телефона")]
		[RegularExpression(@"^\+[7]\d{10}$", ErrorMessage = "Формат номера телефона +79xxxxxxxxx")]
		public string UserPhone { get; set; }

		public string? EmailUser { get; set; }

	}
}
