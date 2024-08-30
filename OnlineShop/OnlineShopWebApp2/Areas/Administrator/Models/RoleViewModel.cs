using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
	public class RoleViewModel
	{
		[Required(ErrorMessage = "Не указано имя роли")]
		public string Name { get; set; }
	}
}
