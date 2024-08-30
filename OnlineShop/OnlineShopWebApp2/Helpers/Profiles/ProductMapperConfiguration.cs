using AutoMapper;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers.Profiles
{
	public class ProductMapperConfiguration:Profile
	{
		public ProductMapperConfiguration()
		{
			CreateMap<Product, ProductViewModel>()
				.ForMember(p => p.UploadedFiles, x => x.Ignore());
			CreateMap<ProductViewModel, Product>()
				.ForMember(p => p.CartProducts, x => x.Ignore())
                .ForMember(p => p.Favorites, x => x.Ignore())
                .ForMember(p => p.Comparisons, x => x.Ignore());
				

		}
	}
}
