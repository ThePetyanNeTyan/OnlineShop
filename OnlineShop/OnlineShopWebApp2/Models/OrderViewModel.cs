using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{

    public class OrderViewModel
    {
		public Guid Id { get; set; }

		public OrderDetailsViewModel OrderDetails { get; set; } = new();

		public string Date { get => DateTime.Today.ToString("d"); }

		public string Time { get => DateTime.Now.ToLongTimeString(); }

		public List<CartProductViewModel> CartProducts { get; set; } = new();

		public OrderStatusViewModel Status { get; set; }

		public string UserId { get; set; }

		public OrderViewModel()
        {
            Status = OrderStatusViewModel.Created;
        }
    }
}
