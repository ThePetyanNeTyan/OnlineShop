using AutoMapper;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers.Profiles
{
	public class OrderMapperConfiguration:Profile
	{
		public OrderMapperConfiguration()
		{
			CreateMap<Order, OrderViewModel>().ReverseMap();
		}
	}
}
