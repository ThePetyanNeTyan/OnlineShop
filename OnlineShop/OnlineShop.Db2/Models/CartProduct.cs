
using System;
namespace OnlineShop.Db.Models
{
	public class CartProduct
	{
		public Guid Id { get; set; }
	
		public Guid ProductId { get; set; }
		public Product Product { get; set; }

		public Guid? CartId { get; set; }

		public Cart Cart { get; set; }

		public int? Amount { get; set; }
	}
}
