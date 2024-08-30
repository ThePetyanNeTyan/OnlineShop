using System;
namespace OnlineShop.Db.Models
{
	public class OrderDetails
	{
		public Guid Id { get; set; }
		public Guid OrderId { get; set; }

		public Order Order { get; set; }
		public string UserAdress { get; set; }

		public string UserName { get; set; }

		public string UserPhone { get; set; }

		public string EmailUser { get; set; }

	}
}
