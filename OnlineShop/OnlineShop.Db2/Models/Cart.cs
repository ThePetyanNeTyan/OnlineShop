using System;
using System.Collections.Generic;


namespace OnlineShop.Db.Models
{
	public class Cart
	{
		public string? Id { get; set; }

		public List<CartProduct> CartProducts { get; set; } = new();

	}
}
