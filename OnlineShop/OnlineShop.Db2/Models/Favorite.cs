using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
	public class Favorite
	{
		public string? Id { get; set; }

		public List<Product> Products { get; set; } = new();
	}
}
