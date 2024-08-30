using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Order
    {
		public Guid Id { get; set; }

		public OrderDetails OrderDetails { get; set; }

        public string Date { get => DateTime.Today.ToString("d"); }

        public string Time { get => DateTime.Now.ToLongTimeString(); }

		public List<CartProduct> CartProducts { get; set; } = new();

		public OrderStatus Status { get; set; }

		public string UserId { get; set; }
    }
}
