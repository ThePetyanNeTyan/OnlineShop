using AutoMapper;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers.Profiles
{
	public class CartMapperConfiguration:Profile
	{
		public CartMapperConfiguration() 
		{
			CreateMap<Cart, CartViewModel>()
				.ForMember(p => p.AmountProducts, x => x.Ignore())
				.ForMember(p => p.TotalPrice, x => x.Ignore())
				.ForMember(p => p.TotalBonus, x => x.Ignore());
			CreateMap<CartViewModel, Cart>();
		}

	}
}
