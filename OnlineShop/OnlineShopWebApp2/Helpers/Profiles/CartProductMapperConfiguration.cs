using AutoMapper;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers.Profiles
{
	public class CartProductMapperConfiguration:Profile
	{
		public CartProductMapperConfiguration() 
		{
			CreateMap<CartProduct, CartProductViewModel>()
				.ForMember(p => p.Cost, x => x.Ignore())
				.ForMember(p => p.Bonus, x => x.Ignore());

			CreateMap<CartProductViewModel, CartProduct>()
				.ForMember(p => p.CartId, x => x.Ignore())
				.ForMember(p => p.Cart, x => x.Ignore())
				.ForMember(p => p.Id, x => x.Ignore())
				.ForMember(p => p.ProductId, x => x.Ignore());
		}
	}
}
